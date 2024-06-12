using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VTET.Business;
using VTET.Data.Models;
using Models = VTET.Data.Models;
namespace VTET.RazorWebApp.Pages.Watch
{
    public class WatchDetailModel : PageModel
    {
        private readonly IEvaluationBusiness _evaluationBusiness;
        private readonly IWatchBusiness _watchBusiness;
        public string Message { get; set; } = default;
        public WatchDetailModel(IEvaluationBusiness evaluationBusiness, IWatchBusiness watchBusiness)
        {

            _evaluationBusiness = evaluationBusiness;
            _watchBusiness = watchBusiness;
        }

        public Models.Watch Watches { get; set; }
        [BindProperty]
        public Models.Evaluation Evaluations { get; set; }
        //watch detail here
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var watchResult = await _watchBusiness.GetByIdAsync(id);
            if (watchResult == null || watchResult.Data == null)
            {
                return NotFound();
            }

            Watches = watchResult.Data as Models.Watch;

            return Page();
        }

        //add evaluation
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reload the watch in case of error to re-populate the page
                //Watches = await _context.Watches.FirstOrDefaultAsync(m => m.Id == Evaluations.WatchId);
                return Page();
            }

            await this.SaveEvaluation();
            //await _context.SaveChangesAsync();
            return RedirectToPage("./Evaluation");
        }
        private async Task SaveEvaluation()
        {


            // Assuming the Save method accepts email as an additional parameter
            var evaluationResult = await _evaluationBusiness.Save(this.Evaluations);

            if (evaluationResult != null)
            {
                this.Message = evaluationResult.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
    }
}

