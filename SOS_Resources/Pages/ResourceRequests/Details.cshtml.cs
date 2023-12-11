using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SOS_Resources.Data;
using SOS_Resources.Models;

namespace SOS_Resources.Pages.ResourceRequests
{
    public class DetailsModel : PageModel
    {
        private readonly SOS_Resources.Data.ApplicationDbContext _context;

        public DetailsModel(SOS_Resources.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public ResourceRequest ResourceRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ResourceRequests == null)
            {
                return NotFound();
            }

            var resourcerequest = await _context.ResourceRequests
                .AsNoTracking()
                .Include(r => r.Requester)
                .Include(r => r.Resources)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (resourcerequest == null)
            {
                return NotFound();
            }
            else 
            {
                ResourceRequest = resourcerequest;
            }
            return Page();
        }
    }
}
