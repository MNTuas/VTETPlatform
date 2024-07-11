using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VTET.Business;
using VTET.Common;
using VTET.Data.Models;
using Models = VTET.Data.Models;

namespace VTET.RazorWebApp.Pages.CustomerPage
{
    public class EditModel : PageModel
    {
        private readonly ICustomerBusiness _customerBusiness;

        public EditModel()
        {
            _customerBusiness = new customerBusiness();
        }
        [BindProperty]
        public Models.Customer Customer { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var customerResult = _customerBusiness.GetById(id);
            if (customerResult.Status <= 0 || customerResult.Result.Data == null)
            {
                return NotFound();
            }

            Customer = (Models.Customer)customerResult.Result.Data;


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updateResult = await _customerBusiness.Update(Customer);
            if (updateResult.Status == Const.SUCCESS_UPDATE_CODE)
            {
                return RedirectToPage("./Index");
            }

            else
            {

                return Page();
            }
        }

        private bool CustomerExists(int id)
        {
            var result = _customerBusiness.GetById(id);
            return result.Status > 0 && result.Result.Data != null;
        }
    }
}
