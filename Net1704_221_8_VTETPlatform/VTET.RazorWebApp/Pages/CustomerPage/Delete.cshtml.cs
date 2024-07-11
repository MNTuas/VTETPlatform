using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VTET.Business;
using Models = VTET.Data.Models;

namespace VTET.RazorWebApp.Pages.CustomerPage
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerBusiness _customerBusiness;

        public DeleteModel()
        {
            _customerBusiness ??= new customerBusiness();
        }

        [BindProperty]
        public Models.Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var customerResult = _customerBusiness.GetById(id);
            if (customerResult.Status > 0 && customerResult.Result.Data != null)
            {
                Customer = (Models.Customer)customerResult.Result.Data;
                return Page();
            }
            else
            {
                return NotFound();
            }

        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerResult = await _customerBusiness.Delete(id);
            if (customerResult.Status > 0)
            {
                return RedirectToPage("./Index");
            }


            return Page();

        }
    }
}