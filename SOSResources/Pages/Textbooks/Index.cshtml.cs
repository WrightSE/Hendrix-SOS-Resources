using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SOSResources.Data;
using SOSResources.Models;

namespace SOSResources.Pages.Textbooks
{
    public class IndexModel : PageModel
    {
        private readonly SOSResources.Data.Textbooks _context;

        public IndexModel(SOSResources.Data.Textbooks context)
        {
            _context = context;
        }

        public IList<Textbook> Textbook { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Textbook != null)
            {
                Textbook = await _context.Textbook.ToListAsync();
            }
        }
    }
}
