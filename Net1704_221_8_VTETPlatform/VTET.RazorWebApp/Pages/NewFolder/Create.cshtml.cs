using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VTET.Data.Models;

namespace VTET.RazorWebApp.Pages.NewFolder
{
    public class CreateModel : PageModel
    {
        private readonly VTET.Data.Models.Net1704_221_8_VTETPlatformContext _context;

        public CreateModel(VTET.Data.Models.Net1704_221_8_VTETPlatformContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName");
            return Page();
        }

        [BindProperty]
        public Watch Watch { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Watches.Add(Watch);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
