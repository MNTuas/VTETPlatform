using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VTET.Business;
using VTET.Data.Models;

namespace VTET.RazorWebApp.Pages
{
    public class EvaluationModel : PageModel
    {
        private readonly IEvaluationBusiness _evaluationBusiness = new evaluationBusiness();

        private readonly IWatchBusiness _watchBusiness = new watchBusiness();
        [BindProperty]
        public Evaluation Evaluation { get; set; } = default;
        public List<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

        public Watch Watch { get; set; } = default;
        public List<Watch> Watchs { get; set; } = new List<Watch>();

        public string Message { get; set; }
        public string Fullname;

        public void OnGet()
        {
            Fullname = HttpContext.Session.GetString("email");
            Evaluations = this.GetEvaluation();
            Watchs = this.GetWatch();
        }

        //update evaluation
        public async Task<IActionResult> OnPostEditAsync()
        {
            if (ModelState.IsValid)
            {
                await this.UpdateEvaluation();
                return RedirectToPage("./Evaluation");
            }
            return Page();
        }
        private async Task UpdateEvaluation()
        {
            var evaluationResult = _evaluationBusiness.Update(this.Evaluation);

            if (evaluationResult != null)
            {
                this.Message = evaluationResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }

        //delete evaluation
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await this.DeleteEvaluation(id);
            return RedirectToPage("./Evaluation");
        }
        private async Task DeleteEvaluation(int id)
        {
            var result = await _evaluationBusiness.Delete(id);
            if (result != null)
            {
                this.Message = result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }

        //Evaluation here
        private List<Evaluation> GetEvaluation()
        {
            var evaluationResult = _evaluationBusiness.GetAll();

            if (evaluationResult.Status > 0 && evaluationResult.Result.Data != null)
            {
                var evaluation = (List<Evaluation>)evaluationResult.Result.Data;
                return evaluation;
            }
            return new List<Evaluation>();
        }
        public async Task<IActionResult> OnGetEditAsync(int id)
        {
            var evaluationResult = _evaluationBusiness.GetById(id);
            if (evaluationResult.Status > 0 && evaluationResult.Result.Data != null)
            {
                Evaluation = (Evaluation)evaluationResult.Result.Data;
            }
            return Page();
        }

        //Watch here
        private List<Watch> GetWatch()
        {
            var watchResult = _watchBusiness.GetAll();

            if (watchResult.Status > 0 && watchResult.Result.Data != null)
            {
                var watch = (List<Watch>)watchResult.Result.Data;
                return watch;
            }
            return new List<Watch>();
        }
        public async Task<IActionResult> OnGetWatchDetailAsync(int id)
        {
            var watchResult = _watchBusiness.GetById(id);
            if (watchResult.Status > 0 && watchResult.Result.Data != null)
            {
                Watch = (Watch)watchResult.Result.Data;
            }
            return Page();
        }
    }
}
