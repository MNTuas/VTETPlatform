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
    public class IndexModel : PageModel
    {
        private readonly IOrderDetailBusiness _orderdetailbusiness;
        private readonly IOrderBusiness _orderbusiness;
        private readonly IWatchBusiness _watchbusiness;

        public IndexModel()
        {
            _orderdetailbusiness ??= new OrderDetailBusiness();
            _orderbusiness ??= new OrderBusiness();
            _watchbusiness ??= new watchBusiness();
        }


        public IList<Models.OrderDetail> OrderDetail { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await _orderdetailbusiness.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                OrderDetail = result.Data as List<Models.OrderDetail>;
                if (OrderDetail != null)
                {
                    foreach (var item in OrderDetail)
                    {
                        if (item.OrderId.HasValue)
                        {
                            var orderResult = await _orderbusiness.GetById(item.OrderId.Value);
                            if (orderResult != null && orderResult.Status > 0 && orderResult.Data != null)
                            {
                                item.Order = orderResult.Data as Models.Order;
                            }
                        }
                        if (item.WatchId.HasValue)
                        {
                            var watchResult = await _watchbusiness.GetById(item.WatchId.Value);
                            if (watchResult != null && watchResult.Status > 0 && watchResult.Data != null)
                            {

                                item.Watch = watchResult.Data as Models.Watch;

                            }
                        }
                    }
                }
            }
        }
    }
}
