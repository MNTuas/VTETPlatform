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

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;
        public IList<Models.Order> Order { get; set; } = default!;
        public int TotalPages { get; set; }

        public async Task OnGetAsync(string searchField = null, string searchTerm = null, int? pageIndex = 1)
        {
            var result = await _orderBusiness.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                Order = result.Data as List<Models.Order>;
                if (Order != null)
                {

                    if (!string.IsNullOrEmpty(searchTerm) && !string.IsNullOrEmpty(searchField))
                    {
                        switch (searchField)
                        {
                            case "OrderEmail":
                                Order = Order.Where(od => od.Email != null && od.Email != null && od.Email.ToLower().Contains(searchTerm.ToLower())).ToList();
                                break;
                            case "OrderFullName":
                                Order = Order.Where(od => od.FullName != null && od.FullName != null && od.FullName.ToLower().Contains(searchTerm.ToLower())).ToList();
                                break;
                            case "Customer":
                                Order = Order.Where(od => od.Customer != null && !string.IsNullOrEmpty(od.Customer.FullName) && od.Customer.FullName.ToLower().Contains(searchTerm.ToLower())).ToList();
                                break;
                            case "Amount":
                                Order = Order.Where(od => od.PhoneNumber != null && od.PhoneNumber.ToString().ToLower().Contains(searchTerm.ToLower())).ToList();
                                break;
                            case "Price":
                                Order = Order.Where(od => od.TotalPrice != null && od.TotalPrice.ToString().ToLower().Contains(searchTerm.ToLower())).ToList();
                                break;
                           
                            case "Date":
                                Order = Order.Where(od => od.Date != null && od.Date.ToString().ToLower().Contains(searchTerm.ToLower())).ToList();
                                break;
                            case "Address":
                                Order = Order.Where(od => od.Address != null && od.Address != null && od.Address.ToLower().Contains(searchTerm.ToLower())).ToList();
                                break;
                            case "Notes":
                                Order = Order.Where(od => od.Notes != null && od.Notes != null && od.Notes.ToLower().Contains(searchTerm.ToLower())).ToList();
                                break;
                            case "PaymentMethod":
                                Order = Order.Where(od => od.PaymentMethod != null && od.PaymentMethod != null && od.PaymentMethod.ToLower().Contains(searchTerm.ToLower())).ToList();
                                break;
                        }
                    }
                    TotalPages = (int)Math.Ceiling(Order.Count / (double)PageSize);

                    
                    int startIndex = (PageIndex - 1) * PageSize;
                    Order = Order.Skip(startIndex).Take(PageSize).ToList();
                    foreach (var item in Order)
                    {
                        if (item.CustomerId.HasValue)
                        {
                            var customerResult = await _customerBusiness.GetById(item.CustomerId.Value);
                            if (customerResult != null && customerResult.Status > 0 && customerResult.Data != null)
                            {

                                item.Customer = customerResult.Data as Models.Customer;

                            }
                        }
                    }
                }
            }
        }
    }

}
