using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VTET.Business;
using VTET.Data.Models;
using Models = VTET.Data.Models;

namespace VTET.RazorWebApp.Pages.CustomerPage
{
    public class DetailsModel : PageModel
    {
        private readonly ICustomerBusiness _customerBusiness;
        public DetailsModel()
        {
            _customerBusiness ??= new customerBusiness();
        }
        public Models.Customer Customer { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var customerResult = await _customerBusiness.GetById(id);
            if (customerResult != null && customerResult.Status > 0 && customerResult.Data != null)
            {
                Customer = customerResult.Data as Models.Customer;
                
            }
            return Page();
        }
    }
}
