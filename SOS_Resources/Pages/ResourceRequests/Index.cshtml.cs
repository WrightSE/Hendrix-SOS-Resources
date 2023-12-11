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
    public class IndexModel : PageModel
    {
        private readonly SOS_Resources.Data.ApplicationDbContext _context;

        public IndexModel(SOS_Resources.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ResourceRequest> ResourceRequest { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ResourceRequests != null)
            {
                ResourceRequest = await _context.ResourceRequests
                    .AsNoTracking()
                    .Include(r => r.Requester)
                    .Include(r => r.Resources)
                    .ToListAsync();
            }
        }
    }
}
