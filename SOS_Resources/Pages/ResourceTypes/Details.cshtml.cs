using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SOS_Resources.Data;
using SOS_Resources.Models;

namespace SOS_Resources.Pages.ResourceTypes
{
    public class DetailsModel : PageModel
    {
        private readonly SOS_Resources.Data.ApplicationDbContext _context;

        public DetailsModel(SOS_Resources.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public ResourceType ResourceType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ResourceTypes == null)
            {
                return NotFound();
            }

            var resourcetype = await _context.ResourceTypes.FirstOrDefaultAsync(m => m.ID == id);
            if (resourcetype == null)
            {
                return NotFound();
            }
            else 
            {
                ResourceType = resourcetype;
            }
            return Page();
        }
    }
}
