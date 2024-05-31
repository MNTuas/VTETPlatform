using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VTET.Business;
using VTET.Business.Base;
using VTET.Data.Models;
using Models = VTET.Data.Models;

namespace VTET.RazorWebApp.Pages.Order
{
    public class IndexModel : PageModel
    {
        private readonly IOrderBusiness _orderBusiness = new OrderBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Models.Order Order { get; set; } = default;
        public List<Models.Order> Orders { get; set; } = new List<Models.Order>();
        public void OnGet()
        {
            Orders = this.GetOrders();
        }


        public void OnDelete()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await this.SaveOrder();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await this.DeleteOrder(id);
            return RedirectToPage("./Index");
        }



        public async Task<IActionResult> OnPostEditAsync()
        {
            if (ModelState.IsValid)
            {
                await this.UpdateOrder();
                return RedirectToPage("./Index");
            }
            return Page();
        }
        private List<Models.Order> GetOrders()
        {
            var orderResult = _orderBusiness.GetAll();

            if (orderResult.Status > 0 && orderResult.Result.Data != null)
            {
                var orders = (List<Models.Order>)orderResult.Result.Data;
                return orders;
            }
            return new List<Models.Order>();
        }
        public async Task<IActionResult> OnGetEditAsync(int id)
        {
            var orderResult = _orderBusiness.GetById(id);
            if (orderResult.Status > 0 && orderResult.Result.Data != null)
            {
                Order = (Models.Order)orderResult.Result.Data;
            }
            return Page();
        }

        private async Task SaveOrder()
        {
            var currencyResult = _orderBusiness.Save(this.Order);

            if (currencyResult != null)
            {
                this.Message = currencyResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
        private async Task UpdateOrder()
        {
            var orderResult = _orderBusiness.Update(this.Order);

            if (orderResult != null)
            {
                this.Message = orderResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
        private async Task DeleteOrder(int id)
        {
            var result = await _orderBusiness.Delete(id);
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
