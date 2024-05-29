using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VTET.Business;
using VTET.Business.Base;
using VTET.Data.Models;
using Models = VTET.Data.Models;
namespace VTET.RazorWebApp.Pages.OrderDetail
{
    public class IndexModel : PageModel
    {
        private readonly IOrderDetailBusiness _orderDetailBusiness = new OrderDetailBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Models.OrderDetail OrderDetail { get; set; } = default;
        public List<Models.OrderDetail> OrderDetails { get; set; } = new List<Models.OrderDetail>();
        public void OnGet()
        {
            OrderDetails = this.GetOrderDetails();
        }

     
        public void OnDelete()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await this.SaveOrderDetail();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            await this.DeleteOrderDetail(id);
            return RedirectToPage("./Index");
        }



        public async Task<IActionResult> OnPostEditAsync()
        {
            if (ModelState.IsValid)
            {
                await this.UpdateOrderDetail();
                return RedirectToPage("./Index");
            }
            return Page();
        }
        private List<Models.OrderDetail> GetOrderDetails()
        {
            var orderdetailResult = _orderDetailBusiness.GetAll();

            if (orderdetailResult.Status > 0 && orderdetailResult.Result.Data != null)
            {
                var orderdetails = (List<Models.OrderDetail>)orderdetailResult.Result.Data;
                return orderdetails;
            }
            return new List<Models.OrderDetail>();
        }
        public async Task<IActionResult> OnGetEditAsync(string id)
        {
            var orderDetailResult =  _orderDetailBusiness.GetById(id);
            if (orderDetailResult.Status > 0 && orderDetailResult.Result.Data != null)
            {
                OrderDetail = (Models.OrderDetail)orderDetailResult.Result.Data;
            }
            return Page();
        }

        private async Task SaveOrderDetail()
        {
            var currencyResult = _orderDetailBusiness.Save(this.OrderDetail);

            if (currencyResult != null)
            {
                this.Message = currencyResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
        private async Task UpdateOrderDetail()
        {
            var orderDetailResult = _orderDetailBusiness.Update(this.OrderDetail);

            if (orderDetailResult != null)
            {
                this.Message = orderDetailResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
        private async Task DeleteOrderDetail(string id)
        {
            var result = await _orderDetailBusiness.Delete(id);

            if (result != null)
            {
                this.Message = result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }

    }
}
