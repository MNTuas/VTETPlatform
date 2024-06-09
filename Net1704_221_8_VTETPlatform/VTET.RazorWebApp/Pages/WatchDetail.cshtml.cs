using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VTET.Business;
using VTET.Data.Models;

namespace VTET.RazorWebApp.Pages
{
    public class WatchDetailModel : PageModel
    {
        private readonly Net1704_221_8_VTETPlatformContext _context;
        private readonly IEvaluationBusiness _evaluationBusiness;
        private readonly IWatchBusiness _watchBusiness;
        public string Message { get; set; } = default;
        public WatchDetailModel(Net1704_221_8_VTETPlatformContext context, IEvaluationBusiness evaluationBusiness, IWatchBusiness watchBusiness)
        {
            _context = context;
            _evaluationBusiness = evaluationBusiness;
            _watchBusiness = watchBusiness;
        }

        public Watch Watches { get; set; } 
        [BindProperty]
        public Evaluation Evaluations { get; set; }
        //watch detail here
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var watchResult = await _watchBusiness.GetByIdAsync(id);
            if (watchResult == null || watchResult.Data == null)
            {
                return NotFound();
            }

            Watches = watchResult.Data as Watch;

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
