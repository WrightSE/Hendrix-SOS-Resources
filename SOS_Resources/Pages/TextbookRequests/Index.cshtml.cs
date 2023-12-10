using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SOS_Resources.Data;
using SOS_Resources.Models;

namespace SOS_Resources.Pages.TextbookRequests
{
    public class IndexModel : PageModel
    {
        private readonly SOS_Resources.Data.ApplicationDbContext _context;

        public IndexModel(SOS_Resources.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TextbookRequest> TextbookRequest { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.TextbookRequests != null)
            {
                TextbookRequest = await _context.TextbookRequests.Include(r => r.Requester).ThenInclude(rq => rq.User).ToListAsync();
            }
        }
    }
}
