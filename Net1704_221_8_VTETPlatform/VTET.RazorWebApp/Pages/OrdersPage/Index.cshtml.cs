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

        [BindProperty(SupportsGet = true)]
        public string OrderEmailSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public string OrderFullNameSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CustomerFullNameSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PhoneNumberSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? TotalPriceSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? DateSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AddressSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NotesSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PaymentMethodSearch { get; set; }

        public int PageSize { get; set; } = 5;
        public IList<Models.Order> Order { get; set; } = default!;
        public int TotalPages { get; set; }

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
                            var customerResult = await _customerBusiness.GetById(item.CustomerId.Value);
                            if (customerResult != null && customerResult.Status > 0 && customerResult.Data != null)
                            {
                                item.Customer = customerResult.Data as Models.Customer;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(OrderEmailSearch))
                    {
                        Order = Order.Where(o => o.Email != null && o.Email.ToLower().Contains(OrderEmailSearch.ToLower())).ToList();
                    }

                    if (!string.IsNullOrEmpty(OrderFullNameSearch))
                    {
                        Order = Order.Where(o => o.FullName != null && o.FullName.ToLower().Contains(OrderFullNameSearch.ToLower())).ToList();
                    }

                    if (!string.IsNullOrEmpty(CustomerFullNameSearch))
                    {
                        Order = Order.Where(o => o.Customer != null && o.Customer.FullName != null && o.Customer.FullName.ToLower().Contains(CustomerFullNameSearch.ToLower())).ToList();
                    }

                    if (!string.IsNullOrEmpty(PhoneNumberSearch))
                    {
                        Order = Order.Where(o => o.PhoneNumber != null && o.PhoneNumber.Contains(PhoneNumberSearch)).ToList();
                    }

                    if (TotalPriceSearch.HasValue)
                    {
                        Order = Order.Where(o => o.TotalPrice == TotalPriceSearch.Value).ToList();
                    }

                    if (DateSearch.HasValue)
                    {
                        Order = Order.Where(o => o.Date.HasValue && o.Date.Value.Date == DateSearch.Value.Date).ToList();
                    }

                    if (!string.IsNullOrEmpty(AddressSearch))
                    {
                        Order = Order.Where(o => o.Address != null && o.Address.ToLower().Contains(AddressSearch.ToLower())).ToList();
                    }

                    if (!string.IsNullOrEmpty(NotesSearch))
                    {
                        Order = Order.Where(o => o.Notes != null && o.Notes.ToLower().Contains(NotesSearch.ToLower())).ToList();
                    }

                    if (!string.IsNullOrEmpty(PaymentMethodSearch))
                    {
                        Order = Order.Where(o => o.PaymentMethod != null && o.PaymentMethod.ToLower().Contains(PaymentMethodSearch.ToLower())).ToList();
                    }

                    TotalPages = (int)Math.Ceiling(Order.Count / (double)PageSize);

                    int startIndex = (PageIndex - 1) * PageSize;
                    Order = Order.Skip(startIndex).Take(PageSize).ToList();
                }
            }
        }
    }
}