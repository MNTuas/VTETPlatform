using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VTET.Business;
using Models = VTET.Data.Models;
namespace VTET.RazorWebApp.Pages.Evaluation
{
    public class DetailEvaluationModel : PageModel
    {
        private readonly IEvaluationBusiness _evaluationBusiness = new evaluationBusiness();

        [BindProperty]
        public Models.Evaluation Evaluation { get; set; }

        public string Message { get; set; }
        public DetailEvaluationModel(IEvaluationBusiness evaluationBusiness)
        {
            _evaluationBusiness ??= evaluationBusiness;
        }
        public async Task<IActionResult> OnGetEvaluationDetailAsync(int id)
        {
            var evaluationResult = _evaluationBusiness.GetByIdAsync(id);
            if (evaluationResult.Status > 0 && evaluationResult.Result.Data != null)
            {
                Evaluation = (Models.Evaluation)evaluationResult.Result.Data;
            }
            return Page();
        }

    }
}

