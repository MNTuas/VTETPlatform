using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VTET.Business;
using VTET.Common;
using VTET.Data.Models;
using Models = VTET.Data.Models;

namespace VTET.RazorWebApp.Pages.OrdersPage
{
    public class CreateModel : PageModel
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly ICustomerBusiness _customerBusiness;
        private readonly IWatchBusiness _watchBusiness;
        private readonly IOrderDetailBusiness _orderdetailBusiness;
        public CreateModel(VTET.Data.Models.Net1704_221_8_VTETPlatformContext context)
        {
            _orderBusiness ??= new OrderBusiness();
            _customerBusiness ??= new customerBusiness();
            _watchBusiness ??= new watchBusiness();
            _orderdetailBusiness ??= new OrderDetailBusiness();

        }

        public async Task<IActionResult> OnGetAsync(int? watchId)
        {
            var customerResult = await _customerBusiness.GetAll();

            // Check if the result is successful and has data
            if (customerResult.Status == Const.SUCCESS_READ_CODE && customerResult.Data is List<Models.Customer> customers)
            {
                ViewData["CustomerId"] = new SelectList(customers, "Id", "FullName");
            }
            else
            {
                // Handle error or no data case if needed
                ViewData["CustomerId"] = new SelectList(new List<Models.Customer>(), "Id", "FullName");
            }
            // Khởi tạo Order nếu chưa được khởi tạo
            Order ??= new Models.Order();

            if (watchId.HasValue)
            {
                var watchResult = await _watchBusiness.GetById(watchId.Value);
                if (watchResult.Status == Const.SUCCESS_READ_CODE && watchResult.Data is Models.Watch watch)
                {
                    Watch = watch;
                    Order.TotalPrice = Watch.Price;

                }
            }
            return Page();
        }
        
        [BindProperty] //auto nhan du lieu tu form khi submit
        public Models.Order Order { get; set; } = default!;

        [BindProperty]
        public Models.Watch Watch { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync(int? watchId)
        {
    
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _orderBusiness.Save(Order);
            if (watchId.HasValue)
            {
                var watchResult = await _watchBusiness.GetById(watchId.Value);
                if (watchResult.Status == Const.SUCCESS_READ_CODE && watchResult.Data is Models.Watch watch)
                {
                    
                    Watch = watch;
                    var orderDetail = new Models.OrderDetail
                    {
                        WatchId = Watch.Id,
                        OrderId = Order.Id,
                        Price = Watch.Price.Value,
                        Amount = 1, // Assuming default amount is 1, you can modify as needed
                        Discount = 10,
                        Tax = 10,
                        ShippingCost = Watch.Price.Value * 0.10m,
                        ShipmentDate = Order.Date.Value, // Using Order.Date for ShipmentDate
                        EstimatedDeliveryDate = Order.Date.Value.AddDays(3) // Using Order.Date for EstimatedDeliveryDate
                    };

                    await _orderdetailBusiness.Save(orderDetail);

                }
            }
            return RedirectToPage("./Index");
        }
    }
}
