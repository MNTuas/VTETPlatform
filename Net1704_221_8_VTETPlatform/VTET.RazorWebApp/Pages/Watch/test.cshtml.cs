using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VTET.Business;
using Models = VTET.Data.Models;
namespace VTET.RazorWebApp.Pages.Watch
{
    public class testModel : PageModel
    {
        private readonly IEvaluationBusiness _evaluationBusiness;
        [BindProperty]
        public List<Models.Evaluation> Evaluations { get; set; } = new List<Models.Evaluation>();
        public testModel(IEvaluationBusiness evaluationBusiness)
        {
           _evaluationBusiness ??= evaluationBusiness;
        }

        public async Task OnGetAsync(int id)
        {
            Evaluations = GetEvaluation(id);
        }
        private List<Models.Evaluation> GetEvaluation(int watchId)
        {
            var evaluationResult = _evaluationBusiness.GetAll();

            if (evaluationResult.Status > 0 && evaluationResult.Result.Data != null)
            {                
                var evaluations = (List<Models.Evaluation>)evaluationResult.Result.Data;
                //get watchid here
                var watchEvaResult = evaluations.Where(e => e.WatchId == watchId).ToList();
                return watchEvaResult;
            }
            return new List<Models.Evaluation>();
        }
    }
}

