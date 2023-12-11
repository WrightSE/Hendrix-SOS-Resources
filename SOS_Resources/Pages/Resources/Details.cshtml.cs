using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SOS_Resources.Data;
using SOS_Resources.Models;

namespace SOS_Resources.Pages.Resources
{
    public class DetailsModel : PageModel
    {
        private readonly SOS_Resources.Data.ApplicationDbContext _context;

        public DetailsModel(SOS_Resources.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Resource Resource { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Resources == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources
                .AsNoTracking()
                .Include(r => r.Type)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (resource == null)
            {
                return NotFound();
            }
            else 
            {
                Resource = resource;
            }
            return Page();
        }
    }
}
