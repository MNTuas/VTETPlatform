using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VTET.Business;
using Models = VTET.Data.Models;

namespace VTET.RazorWebApp.Pages.Evaluation
{
    public class AddEvaluationModel : PageModel
    {
        private readonly IEvaluationBusiness _evaluationBusiness;
        private readonly IWatchBusiness _watchBusiness;
        public string Message { get; set; } = default;
        public Models.Watch Watches { get; set; }
        [BindProperty]
        public Models.Evaluation Evaluations { get; set; }
        public AddEvaluationModel(IEvaluationBusiness evaluationBusiness, IWatchBusiness watchBusiness)
        {
            _evaluationBusiness ??= evaluationBusiness;
            _watchBusiness ??= watchBusiness;
        }    
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

            await SaveEvaluation();
            //await _context.SaveChangesAsync();
            return RedirectToPage("/Evaluation");
        }
        private async Task SaveEvaluation()
        {


            // Assuming the Save method accepts email as an additional parameter
            var evaluationResult = await _evaluationBusiness.Save(Evaluations);

            if (evaluationResult != null)
            {
                Message = evaluationResult.Message;
            }
            else
            {
                Message = "Error system";
            }
        }
    }
}
