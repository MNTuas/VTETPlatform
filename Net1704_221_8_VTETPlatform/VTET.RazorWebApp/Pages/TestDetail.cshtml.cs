using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VTET.Business;
using Models = VTET.Data.Models;

namespace VTET.RazorWebApp.Pages
{
    public class TestDetailModel : PageModel
    {
        private readonly IEvaluationBusiness _evaluationBusiness = new evaluationBusiness();

        [BindProperty]
        public Models.Evaluation Evaluation { get; set; }

        public string Message { get; set; }
        public TestDetailModel(IEvaluationBusiness evaluationBusiness)
        {
            _evaluationBusiness = evaluationBusiness;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var watchResult = await _evaluationBusiness.GetByIdAsync(id);
            if (watchResult == null || watchResult.Data == null)
            {
                return NotFound();
            }

            Evaluation = watchResult.Data as Models.Evaluation;

            return Page();
        }

    }
}
