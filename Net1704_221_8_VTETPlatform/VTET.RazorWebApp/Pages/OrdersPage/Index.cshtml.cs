using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VTET.Business;
using VTET.Data.Models;
using static VTET.Business.CustomerBusiness;
using Models = VTET.Data.Models;


namespace VTET.RazorWebApp.Pages.OrdersPage
{
    public class IndexModel : PageModel
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly ICustomerBusiness _customerBusiness;

        public IndexModel()
        {
            _orderBusiness ??= new OrderBusiness();
            _customerBusiness ??= new customerBusiness();
        }


        public IList<Models.Order> Order { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await _orderBusiness.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                Order = result.Data as List<Models.Order>;
                if (Order != null)
                {
                    foreach (var item in Order)
                    {
                        if (item.CustomerId.HasValue)
                        {
                            var watchResult = await _customerBusiness.GetById(item.CustomerId.Value);
                            if (watchResult != null && watchResult.Status > 0 && watchResult.Data != null)
                            {

                                item.Customer = watchResult.Data as Models.Customer;

                            }
                        }
                    }
                }
            }
        }
    }

}
