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
    public class DeleteModel : PageModel
    {
        private readonly SOS_Resources.Data.ApplicationDbContext _context;

        public DeleteModel(SOS_Resources.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TextbookRequest TextbookRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TextbookRequests == null)
            {
                return NotFound();
            }

            var textbookrequest = await _context.TextbookRequests.FirstOrDefaultAsync(m => m.ID == id);

            if (textbookrequest == null)
            {
                return NotFound();
            }
            else 
            {
                TextbookRequest = textbookrequest;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TextbookRequests == null)
            {
                return NotFound();
            }
            var textbookrequest = await _context.TextbookRequests.FindAsync(id);

            if (textbookrequest != null)
            {
                TextbookRequest = textbookrequest;
                _context.TextbookRequests.Remove(TextbookRequest);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
