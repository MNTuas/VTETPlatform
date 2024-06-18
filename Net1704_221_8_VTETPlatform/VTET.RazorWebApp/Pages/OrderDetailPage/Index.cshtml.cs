using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;
        public IList<Models.OrderDetail> OrderDetail { get; set; } = default!;
        public int TotalPages { get; set; }

        public async Task OnGetAsync(string searchField = null, string searchTerm = null, int? pageIndex = 1)
        {
            var result = await _orderdetailbusiness.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                OrderDetail = result.Data as List<Models.OrderDetail>;
                if (OrderDetail != null)
                {
                    // Nạp dữ liệu Order và Watch trước khi tìm kiếm
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

                    if (!string.IsNullOrEmpty(searchTerm) && !string.IsNullOrEmpty(searchField))
                    {
                        switch (searchField)
                        {
                            case "OrderFullName":
                                OrderDetail = OrderDetail.Where(od => od.Order != null && od.Order.FullName != null && od.Order.FullName.ToLower().Contains(searchTerm.ToLower())).ToList();
                                break;
                            case "WatchFullName":
                                OrderDetail = OrderDetail.Where(od => od.Watch != null && od.Watch.FullName != null && od.Watch.FullName.ToLower().Contains(searchTerm.ToLower())).ToList();
                                break;
                            case "Amount":
                                OrderDetail = OrderDetail.Where(od => od.Amount.ToString().ToLower().Contains(searchTerm.ToLower())).ToList();
                                break;
                            case "Price":
                                OrderDetail = OrderDetail.Where(od => od.Price.ToString().ToLower().Contains(searchTerm.ToLower())).ToList();
                                break;
                            case "ShipmentDate":
                                OrderDetail = OrderDetail.Where(od => od.ShipmentDate.ToString().ToLower().Contains(searchTerm.ToLower())).ToList();
                                break;
                        }
                    }

                    TotalPages = (int)Math.Ceiling(OrderDetail.Count / (double)PageSize);

                    // Lấy chỉ mục trang hiện tại
                    int startIndex = (PageIndex - 1) * PageSize;
                    OrderDetail = OrderDetail.Skip(startIndex).Take(PageSize).ToList();
                }
            }
        }
    }
}
