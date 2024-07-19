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
namespace VTET.RazorWebApp.Pages.Watch
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
        public Models.Watch Watch { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var watchResult = _watchbusiness.GetById(id ?? default(int));
            if (watchResult.Status <= 0 || watchResult.Result.Data == null)
            {
                return NotFound();
            }

            Watch = (Models.Watch)watchResult.Result.Data;


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

            var updateResult = await _watchbusiness.Update(Watch);
            if (updateResult.Status == Const.SUCCESS_UPDATE_CODE)
            {
                return RedirectToPage("/HomePage");
            }

            else
            {

                return Page();
            }
        }

        private bool WatchExists(int id)
        {
            var result = _watchbusiness.GetById(id);
            return result.Status > 0 && result.Result.Data != null;
        }
    }
}
