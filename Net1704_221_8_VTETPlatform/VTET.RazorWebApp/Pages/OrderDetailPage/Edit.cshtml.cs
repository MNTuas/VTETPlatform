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
    public class EditModel : PageModel
    {
        private readonly IOrderDetailBusiness _orderdetailbusiness;
        private readonly IOrderBusiness _orderbusiness;
        private readonly IWatchBusiness _watchbusiness;

        public EditModel()
        {
            _orderdetailbusiness = new OrderDetailBusiness();
            _orderbusiness = new OrderBusiness();
            _watchbusiness = new watchBusiness();
        }

        [BindProperty]
        public Models.OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var orderDetailResult = _orderdetailbusiness.GetById(id);
            if (orderDetailResult.Status <= 0 || orderDetailResult.Result.Data == null)
            {
                return NotFound();
            }

            OrderDetail = (Models.OrderDetail)orderDetailResult.Result.Data;

            var ordersResult = await _orderbusiness.GetAll();
            if (ordersResult.Status == Const.SUCCESS_READ_CODE && ordersResult.Data is List<Models.Order> orders)
            {
                ViewData["OrderId"] = new SelectList(orders, "Id", "FullName");
            }
            else
            {
                ViewData["OrderId"] = new SelectList(new List<Models.Order>(), "Id", "FullName");
            }


            var watchesResult = await _watchbusiness.GetAll();
            if (watchesResult.Status == Const.SUCCESS_READ_CODE && watchesResult.Data is List<Models.Watch> watches)
            {
                ViewData["WatchId"] = new SelectList(watches, "Id", "FullName");
            }
            else
            {
                ViewData["WatchId"] = new SelectList(new List<Models.Watch>(), "Id", "FullName");
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

            var updateResult = await _orderdetailbusiness.Update(OrderDetail);
            if (updateResult.Status == Const.SUCCESS_UPDATE_CODE)
            {
                return RedirectToPage("./Index");
            }

            else
            {
               
                return Page();
            }
        }

        private bool OrderDetailExists(int id)
        {
            var result = _orderdetailbusiness.GetById(id);
            return result.Status > 0 && result.Result.Data != null;
        }
    }
}
