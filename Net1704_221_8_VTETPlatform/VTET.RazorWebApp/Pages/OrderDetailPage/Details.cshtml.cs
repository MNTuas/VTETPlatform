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

namespace VTET.RazorWebApp.Pages.OrderDetailPage
{
    public class DetailsModel : PageModel
    {
        private readonly IOrderDetailBusiness _orderdetailbusiness;
        private readonly IOrderBusiness _orderbusiness;
        private readonly IWatchBusiness _watchbusiness;
        public DetailsModel()
        {
            _orderdetailbusiness ??= new OrderDetailBusiness();
            _orderbusiness ??= new OrderBusiness();
            _watchbusiness ??= new watchBusiness();
        }

        public Models.OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var orderDetailResult = await _orderdetailbusiness.GetById(id);
            if (orderDetailResult != null && orderDetailResult.Status > 0 && orderDetailResult.Data != null)
            {
                OrderDetail = orderDetailResult.Data as Models.OrderDetail;
                if (OrderDetail != null)
                {
                    if (OrderDetail.OrderId.HasValue)
                    {
                        var orderResult = await _orderbusiness.GetById(OrderDetail.OrderId.Value);
                        if (orderResult != null && orderResult.Status > 0 && orderResult.Data != null)
                        {
                            OrderDetail.Order = orderResult.Data as Models.Order;
                        }
                    }

                    if (OrderDetail.WatchId.HasValue)
                    {
                        var watchResult = await _watchbusiness.GetById(OrderDetail.WatchId.Value);
                        if (watchResult != null && watchResult.Status > 0 && watchResult.Data != null)
                        {
                            OrderDetail.Watch = watchResult.Data as Models.Watch;
                        }
                    }
                }
            }
            return Page();
        }
    }
}
