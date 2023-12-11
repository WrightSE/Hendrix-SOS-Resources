using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SOSResources.Data;
using SOSResources.Models;

namespace SOSResources.Pages.ResourceRequests
{
    public class IndexModel : PageModel
    {
        private readonly SOSResources.Data.SOSContext _context;

        public IndexModel(SOSResources.Data.SOSContext context)
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
