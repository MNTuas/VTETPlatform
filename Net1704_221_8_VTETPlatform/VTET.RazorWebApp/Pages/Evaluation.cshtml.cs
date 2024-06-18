using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VTET.Business;
using VTET.Data.Models;
using Models = VTET.Data.Models;

namespace VTET.RazorWebApp.Pages
{
    public class EvaluationModel : PageModel
    {
        private readonly IEvaluationBusiness _evaluationBusiness;

        private readonly IWatchBusiness _watchBusiness;
        [BindProperty]
        public Models.Evaluation Evaluation { get; set; } = default;
        public List<Models.Evaluation> Evaluations { get; set; } = new List<Models.Evaluation>();

        public Models.Watch Watch { get; set; } = default;
        public List<Models.Watch> Watchs { get; set; } = new List<Models.Watch>();

        public string Message { get; set; }
        public string Fullname;
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10; // Number of records per page
        public int TotalPages { get; set; }

        //


        [BindProperty]
        public string SearchComment { get; set; } = string.Empty;
        [BindProperty]
        public string SearchStatus { get; set; } = string.Empty;
        [BindProperty]
        public string SearchRate { get; set; } = string.Empty;
        [BindProperty]
        public string SearchEvaluationType { get; set; } = string.Empty;

        public EvaluationModel()
        {
            _evaluationBusiness ??= new evaluationBusiness();
            _watchBusiness ??= new watchBusiness(); 
        }
        public async Task OnGetAsync(int currentPage = 1, string SearchComment = "", string SearchStatus = "", string SearchRate = "", string SearchEvaluationType = "")
        {
            Evaluations = await GetEvaluationAsync(currentPage, SearchComment, SearchStatus, SearchRate, SearchEvaluationType);
            Fullname = HttpContext.Session.GetString("email");
            Watchs = GetWatch();
        }

        

        //update evaluation
        public async Task<IActionResult> OnPostEditAsync()
        {
           
                await UpdateEvaluation();
                return RedirectToPage("./Evaluation");
            
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
        private async Task<List<Models.Evaluation>> GetEvaluationAsync(int currentPage = 1, string SearchComment = "", string SearchStatus = "", string SearchRate = "", string SearchEvaluationType = "")
        {
            CurrentPage = currentPage;
            SearchComment = SearchComment;
            SearchStatus = SearchStatus;
            SearchRate = SearchRate;
            SearchEvaluationType = SearchEvaluationType;

            var evaluationResult = await _evaluationBusiness.GetAll();
            if (evaluationResult == null)
            {
                // Handle the case where evaluationResult is null
                throw new InvalidOperationException("Evaluation result cannot be null.");
            }

            if (evaluationResult.Status > 0 && evaluationResult.Data != null)
            {
                var evaluations = (List<Models.Evaluation>)evaluationResult.Data;
                if (!string.IsNullOrEmpty(SearchComment))
                {
                    evaluations = evaluations.Where(c =>
                        (c.Comment?.ToLower().StartsWith(SearchComment.ToLower()) ?? false) ||
                        (c.Comment?.ToLower().Contains(" " + SearchComment.ToLower()) ?? false)
                    ).ToList();

                }
                if (!string.IsNullOrEmpty(SearchStatus))
                {
                    evaluations = evaluations.Where(c =>
                        (c.Status?.ToLower().StartsWith(SearchStatus.ToLower()) ?? false) ||
                        (c.Status?.ToLower().Contains(" " + SearchStatus.ToLower()) ?? false)
                    ).ToList();

                }
                if (!string.IsNullOrEmpty(SearchRate) && int.TryParse(SearchRate, out int SearchRateInt))
                {
                    evaluations = evaluations.Where(c => c.Rate == SearchRateInt).ToList();
                }

                if (!string.IsNullOrEmpty(SearchEvaluationType))
                {
                    evaluations = evaluations.Where(c =>
                        (c.EvaluationType?.ToLower().StartsWith(SearchEvaluationType.ToLower()) ?? false) ||
                        (c.EvaluationType?.ToLower().Contains(" " + SearchEvaluationType.ToLower()) ?? false)
                    ).ToList();

                }


                int PageSize = 4; // Số mục tối đa trên mỗi trang

                // Đếm tổng số mục
                int totalCount = evaluations.Count;
                TotalPages = (int)System.Math.Ceiling(totalCount / (double)PageSize);

                evaluations = evaluations
                    .Skip((CurrentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();

                return evaluations;
            }
            else
            {
                // Handle the case where evaluationResult.Data is null or Status <= 0
                return new List<Models.Evaluation>();
            }
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
        public async Task<IActionResult> OnGetEvaluationDetailAsync(int id)
        {
            var evaluationResult = _evaluationBusiness.GetByIdAsync(id);
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
