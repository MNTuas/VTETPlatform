using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VTET.Data.Models;

namespace VTET.RazorWebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly Net1704_221_8_VTETPlatformContext _context;

        public LoginModel(Net1704_221_8_VTETPlatformContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Model { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var email = Model.Email;
            var password = Model.Password   ;
            // Assuming you're querying for a customer based on email and password
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email && c.Password == password);


            if (customer != null)
            {
                HttpContext.Session.SetString("email", email);
                if (customer.Role == "Customer")
                {
                    return RedirectToPage("/Order/Index");
                }
                else if (customer.Role == "Admin")
                {
                    return RedirectToPage("/Evaluation");
                }
            } 
                return RedirectToPage("/Login");
        }

    }
}
