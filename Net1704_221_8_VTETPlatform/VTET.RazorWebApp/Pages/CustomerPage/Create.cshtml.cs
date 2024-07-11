using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VTET.Business;
using VTET.Common;
using VTET.Data.Models;
using Models = VTET.Data.Models;

namespace VTET.RazorWebApp.Pages.CustomerPage
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerBusiness _customerBusiness;
        public CreateModel(VTET.Data.Models.Net1704_221_8_VTETPlatformContext context)
        {
            _customerBusiness ??= new customerBusiness();
        }
        [BindProperty]
        public Models.Customer Customer { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _customerBusiness.Save(Customer);

            return RedirectToPage("./Index");
        }
    }
}
