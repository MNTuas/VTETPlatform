using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VTET.Business;
using VTET.Common;
using VTET.Data.Models;

namespace VTET.RazorWebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ICustomerBusiness _customerBusiness;
        public LoginModel(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        //[BindProperty]
        //public Customer Model { get; set; }

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

                var customerResult = await _customerBusiness.Login(Email, Password);


            if (customerResult.Status == Const.SUCCESS_READ_CODE)
            {
                HttpContext.Session.SetString("email", Email);
                var userData = (dynamic)customerResult.Data;
                var role = userData.Role;
                if (role == "Admin")
                {
                   return RedirectToPage("/HomePage");
                }
                else if (role == "Customer")
                {
                    return RedirectToPage("/Evaluation");
                }
                // Optionally set session or other actions on successful login
                
                
            }

            // Optionally add an error message to ModelState
            ModelState.AddModelError(string.Empty, customerResult.Message);
            return Page(); // Stay on the same page to show error message

        }

    }
}
