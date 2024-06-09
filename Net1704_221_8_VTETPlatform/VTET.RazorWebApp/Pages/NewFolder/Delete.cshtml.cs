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
    public class DeleteModel : PageModel
    {
        private readonly VTET.Data.Models.Net1704_221_8_VTETPlatformContext _context;

        public DeleteModel(VTET.Data.Models.Net1704_221_8_VTETPlatformContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watch = await _context.Watches.FindAsync(id);
            if (watch != null)
            {
                Watch = watch;
                _context.Watches.Remove(Watch);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
