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

using static VTET.Business.CustomerBusiness;

namespace VTET.RazorWebApp.Pages.OrdersPage
{
    public class DetailsModel : PageModel
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly ICustomerBusiness _customerBusiness;

        public DetailsModel()
        {
            _orderBusiness ??= new OrderBusiness();
            _customerBusiness ??= new customerBusiness();
        }

        public Models.Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var orderResult = await _orderBusiness.GetById(id);
            if (orderResult != null && orderResult.Status > 0 && orderResult.Data != null)
            {
                Order = orderResult.Data as Models.Order;
                if (Order != null)
                {

                    if (Order.CustomerId.HasValue)
                    {
                        var customerResult = await _customerBusiness.GetById(Order.CustomerId.Value);
                        if (customerResult != null && customerResult.Status > 0 && customerResult.Data != null)
                        {
                            Order.Customer = customerResult.Data as Models.Customer;
                        }
                    }
                }
            }
            return Page();
        }
    }
}
