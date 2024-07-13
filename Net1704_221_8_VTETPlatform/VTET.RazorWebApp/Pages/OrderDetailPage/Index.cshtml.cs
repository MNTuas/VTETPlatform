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

        [BindProperty(SupportsGet = true)]
        public string OrderFullNameSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public string WatchFullNameSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? AmountSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? PriceSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? DiscountSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? ShipmentDateSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EstimatedDeliveryDateSearch { get; set; }

        public int PageSize { get; set; } = 10;
        public IList<Models.OrderDetail> OrderDetail { get; set; } = default!;
        public int TotalPages { get; set; }

        public async Task OnGetAsync()
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

                    if (!string.IsNullOrEmpty(OrderFullNameSearch))
                    {
                        OrderDetail = OrderDetail.Where(od => od.Order != null && od.Order.FullName != null && od.Order.FullName.ToLower().Contains(OrderFullNameSearch.ToLower())).ToList();
                    }

                    if (!string.IsNullOrEmpty(WatchFullNameSearch))
                    {
                        OrderDetail = OrderDetail.Where(od => od.Watch != null && od.Watch.FullName != null && od.Watch.FullName.ToLower().Contains(WatchFullNameSearch.ToLower())).ToList();
                    }

                    if (AmountSearch.HasValue)
                    {
                        OrderDetail = OrderDetail.Where(od => od.Amount == AmountSearch.Value).ToList();
                    }

                    if (PriceSearch.HasValue)
                    {
                        OrderDetail = OrderDetail.Where(od => od.Price == PriceSearch.Value).ToList();
                    }

                    if (DiscountSearch.HasValue)
                    {
                        OrderDetail = OrderDetail.Where(od => od.Discount == DiscountSearch.Value).ToList();
                    }

                    if (ShipmentDateSearch.HasValue)
                    {
                        OrderDetail = OrderDetail.Where(od => od.ShipmentDate.HasValue && od.ShipmentDate.Value.Date == ShipmentDateSearch.Value.Date).ToList();
                    }

                    if (EstimatedDeliveryDateSearch.HasValue)
                    {
                        OrderDetail = OrderDetail.Where(od => od.EstimatedDeliveryDate.HasValue && od.EstimatedDeliveryDate.Value.Date == EstimatedDeliveryDateSearch.Value.Date).ToList();
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
