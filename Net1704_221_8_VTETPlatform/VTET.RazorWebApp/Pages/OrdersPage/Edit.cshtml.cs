using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VTET.Business;
using VTET.Data.Models;
using Models = VTET.Data.Models;

using static VTET.Business.CustomerBusiness;
using VTET.Common;

namespace VTET.RazorWebApp.Pages.OrdersPage
{
    public class EditModel : PageModel
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly ICustomerBusiness _customerBusiness;

        public EditModel()
        {
            _orderBusiness = new OrderBusiness();
            _customerBusiness = new customerBusiness();
        }

        [BindProperty]
        public Models.Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var orderResult = _orderBusiness.GetById(id);
            if (orderResult.Status <= 0 || orderResult.Result.Data == null)
            {
                return NotFound();
            }

            Order = (Models.Order)orderResult.Result.Data;

            var customerResult = await _customerBusiness.GetAll();
            if (customerResult.Status == Const.SUCCESS_READ_CODE && customerResult.Data is List<Models.Customer> customters)
            {
                ViewData["CustomerId"] = new SelectList(customters, "Id", "FullName");
            }
            else
            {
                ViewData["CustomerId"] = new SelectList(new List<Models.Customer>(), "Id", "FullName");
            }

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

            var updateResult = await _orderBusiness.Update(Order);
            if (updateResult.Status == Const.SUCCESS_UPDATE_CODE)
            {
                return RedirectToPage("./Index");
            }

            else
            {

                return Page();
            }
        }

        private bool OrderExists(int id)
        {
            var result = _orderBusiness.GetById(id);
            return result.Status > 0 && result.Result.Data != null;
        }
    }
}
