using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VTET.Data.Models;

namespace VTET.RazorWebApp.Pages.NewFolder
{
    public class EditModel : PageModel
    {
        private readonly VTET.Data.Models.Net1704_221_8_VTETPlatformContext _context;

        public EditModel(VTET.Data.Models.Net1704_221_8_VTETPlatformContext context)
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

            var watch =  await _context.Watches.FirstOrDefaultAsync(m => m.Id == id);
            if (watch == null)
            {
                return NotFound();
            }
            Watch = watch;
           ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName");
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

            _context.Attach(Watch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WatchExists(Watch.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WatchExists(int id)
        {
            return _context.Watches.Any(e => e.Id == id);
        }
    }
}
