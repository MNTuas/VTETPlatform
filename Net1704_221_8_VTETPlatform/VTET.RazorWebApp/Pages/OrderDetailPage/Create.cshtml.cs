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

namespace VTET.RazorWebApp.Pages.OrderDetailPage
{
    public class CreateModel : PageModel
    {
        private readonly IOrderDetailBusiness _orderdetailbusiness;
        private readonly IOrderBusiness _orderBusiness;
        private readonly IWatchBusiness _watchbusiness;


        public CreateModel()
        {
            _orderdetailbusiness ??= new OrderDetailBusiness();
            _orderBusiness ??= new OrderBusiness();
            _watchbusiness ??= new watchBusiness();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var ordersResult = await _orderBusiness.GetAll();

            // Check if the result is successful and has data
            if (ordersResult.Status == Const.SUCCESS_READ_CODE && ordersResult.Data is List<Models.Order> orders)
            {
                ViewData["OrderId"] = new SelectList(orders, "Id", "FullName");
            }
            else
            {
                // Handle error or no data case if needed
                // For example, you might want to log the error or set ViewData["OrderId"] to an empty list
                ViewData["OrderId"] = new SelectList(new List<Models.Order>(), "Id", "FullName");
            }
            var watchResult = await _watchbusiness.GetAll();

            // Check if the result is successful and has data
            if (watchResult.Status == Const.SUCCESS_READ_CODE && watchResult.Data is List<Models.Watch> watchs)
            {
                ViewData["WatchId"] = new SelectList(watchs, "Id", "FullName");
            }
            else
            {
                // Handle error or no data case if needed
                // For example, you might want to log the error or set ViewData["OrderId"] to an empty list
                ViewData["WatchId"] = new SelectList(new List<Models.Watch>(), "Id", "FullName");
            }
            return Page();
        }

        [BindProperty]
        public Models.OrderDetail OrderDetail { get; set; } = default!;
        public Models.Order Order { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _orderdetailbusiness.Save(OrderDetail);

            return RedirectToPage("./Index");
        }
    }
}
