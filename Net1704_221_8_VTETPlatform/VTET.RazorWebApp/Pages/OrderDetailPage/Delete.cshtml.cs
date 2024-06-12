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
    public class DeleteModel : PageModel
    {
        private readonly IOrderDetailBusiness _orderdetailbusiness;

        public DeleteModel()
        {
            _orderdetailbusiness ??= new OrderDetailBusiness();
        }

        [BindProperty]
        public Models.OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var orderDetailResult = _orderdetailbusiness.GetById(id);
            if (orderDetailResult.Status > 0 && orderDetailResult.Result.Data != null)
            {
                OrderDetail = (Models.OrderDetail)orderDetailResult.Result.Data;
                return Page();
            }
            else
            {
                return NotFound();
            }

        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdetailresult = await _orderdetailbusiness.Delete(id);
            if (orderdetailresult.Status > 0)
            {
                return RedirectToPage("./Index");
            }


            return Page();

        }
    }
}
