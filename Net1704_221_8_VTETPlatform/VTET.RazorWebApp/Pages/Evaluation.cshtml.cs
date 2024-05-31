using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VTET.Business;
using VTET.Data.Models;

namespace VTET.RazorWebApp.Pages
{
    public class EvaluationModel : PageModel
    {
        private readonly IEvaluationBusiness _evaluationBusiness = new evaluationBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Evaluation Evaluation { get; set; } = default;
        public List<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

        public void OnGet()
        {
            Evaluations = this.GetCurrencies();
        }

        public void OnPost()
        {
            this.SaveCurrency();
        }

        public void OnDelete()
        {
        }


        private List<Evaluation> GetCurrencies()
        {
            var currencyResult = _evaluationBusiness.GetAll();

            if (currencyResult.Status > 0 && currencyResult.Result.Data != null)
            {
                var currencies = (List<Evaluation>)currencyResult.Result.Data;
                return currencies;
            }
            return new List<Evaluation>();
        }

        private void SaveCurrency()
        {
            var currencyResult = _evaluationBusiness.Save(this.Evaluation);

            if (currencyResult != null)
            {
                this.Message = currencyResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
    }
}
