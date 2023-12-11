using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SOSResources.Data;
using SOSResources.Models;

namespace SOSResources.Pages.Textbooks.Copies
{
    public class CopyDeleteModel : PageModel
    {
        private readonly SOSResources.Data.SOSContext _context;

        public CopyDeleteModel(SOSResources.Data.SOSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Copy Copy { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Copies == null)
            {
                return NotFound();
            }

            var copy = await _context.Copies
                .Include(c => c.textbook)
                .Include(c => c.textbookRequests)
                .ThenInclude(r => r.Requester)
                .FirstOrDefaultAsync(m => m.ID == id);
                
            if (copy == null)
            {
                return NotFound();
            }
            else 
            {
                Copy = copy;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Copies == null)
            {
                return NotFound();
            }
            var copy = await _context.Copies
                .Include(c => c.textbook)
                .Include(c => c.textbookRequests)
                .ThenInclude(r => r.Requester)
                .FirstOrDefaultAsync(m => m.ID == id);

            int tbID = copy.textbook.ID;

            if (copy != null)
            {
                Copy = copy;
                if (copy.textbookRequests.Any()){
                    return Page();
                }
                //tbID = Copy.textbook.ID;
                _context.Copies.Remove(Copy);
                await _context.SaveChangesAsync();
                
            }

            return RedirectToPage("/Textbooks/Delete", new{id = tbID.ToString()});
        }
    }
}
