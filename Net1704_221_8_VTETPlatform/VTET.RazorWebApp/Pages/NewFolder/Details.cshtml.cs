using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VTET.Data.Models;

namespace VTET.RazorWebApp.Pages.NewFolder
{
    public class DetailsModel : PageModel
    {
        private readonly VTET.Data.Models.Net1704_221_8_VTETPlatformContext _context;

        public DetailsModel(VTET.Data.Models.Net1704_221_8_VTETPlatformContext context)
        {
            _context = context;
        }

        public Watch Watch { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watch = await _context.Watches.FirstOrDefaultAsync(m => m.Id == id);
            if (watch == null)
            {
                return NotFound();
            }
            else
            {
                Watch = watch;
            }
            return Page();
        }
    }
}
