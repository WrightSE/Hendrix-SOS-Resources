using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SOS_Resources.Data;
using SOS_Resources.Models;

namespace SOS_Resources.Pages.Textbooks
{
    public class DeleteModel : PageModel
    {
        private readonly SOS_Resources.Data.ApplicationDbContext _context;

        public DeleteModel(SOS_Resources.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Textbook Textbook { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Textbooks == null)
            {
                return NotFound();
            }

            var textbook = await _context.Textbooks
                .Include(t => t.Copies)
                .ThenInclude(c => c.textbookRequests)
                .ThenInclude(r => r.Requester)
                .ThenInclude(rq => rq.SOS_User)
                .FirstOrDefaultAsync(m => m.ID == id);
                
            if (textbook == null)
            {
                return NotFound();
            }
            else 
            {
                Textbook = textbook;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Textbooks == null)
            {
                return NotFound();
            }
            var textbook = await _context.Textbooks
                .Include(t => t.Copies)
                .ThenInclude(c => c.textbookRequests)
                .ThenInclude(r => r.Requester)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (textbook != null)
            {
                Textbook = textbook;
                if (textbook.Copies.Any()){
                    return Page();
                }
                _context.Textbooks.Remove(Textbook);
                await _context.SaveChangesAsync();
                
            }

            return RedirectToPage("./Index");
        }
    }
}
