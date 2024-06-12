using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VTET.Business;
using Models = VTET.Data.Models;

namespace VTET.RazorWebApp.Pages
{
    public class EvaluationModel : PageModel
    {
        private readonly IEvaluationBusiness _evaluationBusiness = new evaluationBusiness();

        private readonly IWatchBusiness _watchBusiness = new watchBusiness();
        [BindProperty]
        public Models.Evaluation Evaluation { get; set; } = default;
        public List<Models.Evaluation> Evaluations { get; set; } = new List<Models.Evaluation>();

        public Models.Watch Watch { get; set; } = default;
        public List<Models.Watch> Watchs { get; set; } = new List<Models.Watch>();

        public string Message { get; set; }
        public string Fullname;

        public void OnGet()
        {
            Fullname = HttpContext.Session.GetString("email");
            Evaluations = GetEvaluation();
            Watchs = GetWatch();
        }

        //update evaluation
        public async Task<IActionResult> OnPostEditAsync()
        {
            if (ModelState.IsValid)
            {
                await UpdateEvaluation();
                return RedirectToPage("./Evaluation");
            }
            return Page();
        }
        private async Task UpdateEvaluation()
        {
            var evaluationResult = _evaluationBusiness.Update(Evaluation);

            if (evaluationResult != null)
            {
                Message = evaluationResult.Result.Message;
            }
            else
            {
                Message = "Error system";
            }
        }

        //delete evaluation
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await DeleteEvaluation(id);
            return RedirectToPage("./Evaluation");
        }
        private async Task DeleteEvaluation(int id)
        {
            var result = await _evaluationBusiness.Delete(id);
            if (result != null)
            {
                Message = result.Message;
            }
            else
            {
                Message = "Error system";
            }
        }

        //Evaluation here
        private List<Models.Evaluation> GetEvaluation()
        {
            var evaluationResult = _evaluationBusiness.GetAll();

            if (evaluationResult.Status > 0 && evaluationResult.Result.Data != null)
            {
                var evaluation = (List<Models.Evaluation>)evaluationResult.Result.Data;
                return evaluation;
            }
            return new List<Models.Evaluation>();
        }
        public async Task<IActionResult> OnGetEditAsync(int id)
        {
            var evaluationResult = _evaluationBusiness.GetById(id);
            if (evaluationResult.Status > 0 && evaluationResult.Result.Data != null)
            {
                Evaluation = (Models.Evaluation)evaluationResult.Result.Data;
            }
            return Page();
        }

        //Watch here
        private List<Models.Watch> GetWatch()
        {
            var watchResult = _watchBusiness.GetAll();

            if (watchResult.Status > 0 && watchResult.Result.Data != null)
            {
                var watch = (List<Models.Watch>)watchResult.Result.Data;
                return watch;
            }
            return new List<Models.Watch>();
        }
        public async Task<IActionResult> OnGetWatchDetailAsync(int id)
        {
            var watchResult = _watchBusiness.GetById(id);
            if (watchResult.Status > 0 && watchResult.Result.Data != null)
            {
                Watch = (Models.Watch)watchResult.Result.Data;
            }
            return Page();
        }
    }
}
