using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VTET.Business;
using Models = VTET.Data.Models;

namespace VTET.RazorWebApp.Pages.Watch
{
    public class AddWatchModel : PageModel
    {
        private readonly IWatchBusiness _watchBusiness;
        public string Message { get; set; } = default;
        [BindProperty]
        public Models.Watch Watches { get; set; } = new Models.Watch();
        //add watch
        public AddWatchModel()
        {
            _watchBusiness ??= new watchBusiness();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await SaveWatch();
            return RedirectToPage("/HomePage");
        }
        private async Task SaveWatch()
        {
            var watchResult = await _watchBusiness.Save(Watches);

            if (watchResult != null)
            {
                Message = watchResult.Message;
            }
            else
            {
                Message = "Error system";
            }
        }
    }
}
