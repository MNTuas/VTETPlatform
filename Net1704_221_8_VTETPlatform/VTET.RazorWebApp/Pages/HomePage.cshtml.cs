using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol;
using VTET.Business;
using Models = VTET.Data.Models;

namespace VTET.RazorWebApp.Pages
{
    public class HomePageModel : PageModel
    {
        private readonly IWatchBusiness _watchBusiness = new watchBusiness();
        public string Message { get; set; } = default;

        [BindProperty]
        public Models.Watch Watch { get; set; } = default;
        public List<Models.Watch> Watches { get; set; } = new List<Models.Watch>();
        public List<Models.Evaluation> Evaluations { get; set; } = new List<Models.Evaluation>();
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10; // Number of records per page
        public int TotalPages { get; set; }
        [BindProperty]
        public string SearchFullName { get; set; } = string.Empty;
        [BindProperty]
        public string SearchType { get; set; } = string.Empty;
        [BindProperty]
        public string SearchPrice { get; set; } = string.Empty;
        [BindProperty]
        public string SearchBrand { get; set; } = string.Empty;
        public async Task OnGetAsync(int currentPage = 1,  string SearchFullName = "", string SearchType = "", string SearchPrice = "", string SearchBrand = "")
        {
            Watches = await GetWatchAsync(currentPage, SearchFullName, SearchType, SearchPrice, SearchBrand);
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await this.SaveWatch();
            return RedirectToPage("./Watch");
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await this.DeleteWatch(id);
            return RedirectToPage("./Watch");
        }
        public async Task<IActionResult> OnPostEditAsync()
        {
            if (ModelState.IsValid)
            {
                await this.UpdateWatch();
                return RedirectToPage("./Watch");
            }
            return Page();
        }
        private async Task<List<Models.Watch>> GetWatchAsync(int currentPage = 1, string SearchFullName = "", string SearchType = "", string SearchPrice = "", string SearchBrand = "")
        {
            CurrentPage = currentPage;
            SearchFullName = SearchFullName;
            SearchType = SearchType;
            SearchPrice = SearchPrice;
            SearchBrand = SearchBrand;

            var watchResult = await _watchBusiness.GetAll();
            if (watchResult == null)
            {
                // Handle the case where watchResult is null
                throw new InvalidOperationException("Evaluation result cannot be null.");
            }
            if (watchResult.Status > 0 && watchResult.Data != null)
            {
                var watches = (List<Models.Watch>)watchResult.Data;
                if (!string.IsNullOrEmpty(SearchFullName))
                {
                    watches = watches.Where(c =>
                        (c.FullName?.ToLower().StartsWith(SearchFullName.ToLower()) ?? false) ||
                        (c.FullName?.ToLower().Contains(" " + SearchFullName.ToLower()) ?? false)
                    ).ToList();

                }
                if (!string.IsNullOrEmpty(SearchType))
                {
                    watches = watches.Where(c =>
                        (c.Type?.ToLower().StartsWith(SearchType.ToLower()) ?? false) ||
                        (c.Type?.ToLower().Contains(" " + SearchType.ToLower()) ?? false)
                    ).ToList();
                }
                if (!string.IsNullOrEmpty(SearchPrice) && decimal.TryParse(SearchPrice, out decimal SearchRateInt))
                {
                    watches = watches.Where(c => c.Price == SearchRateInt).ToList();
                }

                if (!string.IsNullOrEmpty(SearchBrand))
                {
                    watches = watches.Where(c =>
                        (c.Brand?.ToLower().StartsWith(SearchBrand.ToLower()) ?? false) ||
                        (c.Brand?.ToLower().Contains(" " + SearchBrand.ToLower()) ?? false)
                    ).ToList();

                }
                int PageSize = 8; // Số mục tối đa trên mỗi trang
                // Đếm tổng số mục
                int totalCount = watches.Count;
                TotalPages = (int)System.Math.Ceiling(totalCount / (double)PageSize);

                watches = watches
                        .Skip((CurrentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();

                return watches;
            }
            else
            {
                // Handle the case where evaluationResult.Data is null or Status <= 0
                return new List<Models.Watch>();
            }
        }
        public async Task<IActionResult> OnGetEditAsync(int id)
        {
            var watchResult = _watchBusiness.GetById(id);
            if (watchResult.Status > 0 && watchResult.Result.Data != null)
            {
                Watch = (Models.Watch)watchResult.Result.Data;
            }
            return Page();
        }
        private async Task SaveWatch()
        {
            var currencyResult = _watchBusiness.Save(this.Watch);

            if (currencyResult != null)
            {
                this.Message = currencyResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
        private async Task UpdateWatch()
        {
            var watchResult = _watchBusiness.Update(this.Watch);

            if (watchResult != null)
            {
                this.Message = watchResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
        private async Task DeleteWatch(int id)
        {
            var result = await _watchBusiness.Delete(id);
            if (result != null)
            {
                this.Message = result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }

    }

}
