using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VTET.Business;
using VTET.Data.Models;
using Models = VTET.Data.Models;


namespace VTET.RazorWebApp.Pages.OrdersPage
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly ICustomerBusiness _customerBusiness;
        private readonly IOrderDetailBusiness _orderDetailBusiness;

        public DeleteModel()
        {
            _orderBusiness ??= new OrderBusiness();
            _customerBusiness ??= new customerBusiness();
            _orderDetailBusiness ??= new OrderDetailBusiness();
        }

        [BindProperty]
        public Models.Order Order { get; set; } = default!;
        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var orderResult = _orderBusiness.GetById(id);
            if (orderResult.Status > 0 && orderResult.Result.Data != null)
            {
                Order = (Models.Order)orderResult.Result.Data;

                if (Order.CustomerId.HasValue)
                {
                    var customerResult = await _customerBusiness.GetById(Order.CustomerId.Value);
                    if (customerResult != null && customerResult.Status > 0 && customerResult.Data != null)
                    {
                        Order.Customer = customerResult.Data as Models.Customer;
                    }
                }

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
            var orderResult = await _orderBusiness.Delete(id);
            if (orderResult.Status > 0)
            {
                return RedirectToPage("./Index");
            }

            ErrorMessage = "Cannot delete due to own Order Detail";
            return await OnGetAsync(id);
        }
    }
}
