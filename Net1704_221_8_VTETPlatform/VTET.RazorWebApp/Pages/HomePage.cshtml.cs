using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public void OnGet()
        {
            Watches = this.GetWatch();
        }


        public void OnDelete()
        {

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
        private List<Models.Watch> GetWatch()
        {
            var watchResult = _watchBusiness.GetAll();

            if (watchResult.Status > 0 && watchResult.Result.Data != null)
            {
                var Watchess = (List<Models.Watch>)watchResult.Result.Data;
                return Watchess;
            }
            return new List<Models.Watch>();
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
