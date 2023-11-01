using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SOSResources.Data;
using SOSResources.Models;

namespace SOSResources.Pages.Resources
{
    public class IndexModel : PageModel
    {
        private readonly SOSResources.Data.Resources _context;

        public IndexModel(SOSResources.Data.Resources context)
        {
            _context = context;
        }

        public IList<Resource> Resource { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Resource != null)
            {
                Resource = await _context.Resource.ToListAsync();
            }
        }
    }
}
