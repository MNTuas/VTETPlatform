﻿using System;
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
using static VTET.Business.CustomerBusiness;

namespace VTET.RazorWebApp.Pages.OrdersPage
{
    public class CreateModel : PageModel
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly ICustomerBusiness _customerBusiness;

        public CreateModel(VTET.Data.Models.Net1704_221_8_VTETPlatformContext context)
        {
            _orderBusiness ??= new OrderBusiness();
            _customerBusiness ??= new customerBusiness();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var ordersResult = await _orderBusiness.GetAll();

            // Check if the result is successful and has data
            if (ordersResult.Status == Const.SUCCESS_READ_CODE && ordersResult.Data is List<Models.Order> orders)
            {
                ViewData["OrderId"] = new SelectList(orders, "Id", "Id");
            }
            else
            {
                // Handle error or no data case if needed
                // For example, you might want to log the error or set ViewData["OrderId"] to an empty list
                ViewData["OrderId"] = new SelectList(new List<Models.Order>(), "Id", "Id");
            }
            var customerResult = await _customerBusiness.GetAll();

            // Check if the result is successful and has data
           /* if (customerBusiness.Status == Const.SUCCESS_READ_CODE && watchResult.Data is List<Models.Watch> watchs)
            {
                ViewData["WatchId"] = new SelectList(watchs, "Id", "FullName");
            }
            else
            {
                // Handle error or no data case if needed
                // For example, you might want to log the error or set ViewData["OrderId"] to an empty list
                ViewData["WatchId"] = new SelectList(new List<Models.Watch>(), "Id", "FullName");
            }*/
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

            await _orderBusiness.Save(Order);

            return RedirectToPage("./Index");
        }
    }
}
