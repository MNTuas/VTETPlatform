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
        public Models.Watch Watches { get; set; }
        [BindProperty]
        public Models.Evaluation Evaluations { get; set; }
        public WatchDetailModel()
        {
            _evaluationBusiness ??= new evaluationBusiness();
            _watchBusiness ??= new watchBusiness();
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
    }
}

