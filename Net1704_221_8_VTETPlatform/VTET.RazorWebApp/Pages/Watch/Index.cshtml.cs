using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VTET.Data.Models;
using Models = VTET.Data.Models;

namespace VTET.RazorWebApp.Pages.Watch
{
    public class IndexModel : PageModel
    {
        private readonly VTET.Data.Models.Net1704_221_8_VTETPlatformContext _context;

        public IndexModel(VTET.Data.Models.Net1704_221_8_VTETPlatformContext context)
        {
            _context = context;
        }

        public IList<Models.Watch> Watch { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Watch = await _context.Watches
                .Include(w => w.Customer).ToListAsync();
        }
    }
}
