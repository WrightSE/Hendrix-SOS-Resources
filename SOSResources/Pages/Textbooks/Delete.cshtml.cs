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
    public class DeleteModel : PageModel
    {
        private readonly SOSResources.Data.Textbooks _context;

        public DeleteModel(SOSResources.Data.Textbooks context)
        {
            _context = context;
        }

        [BindProperty]
      public Textbook Textbook { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Textbook == null)
            {
                return NotFound();
            }

            var textbook = await _context.Textbook.FirstOrDefaultAsync(m => m.ID == id);

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
            if (id == null || _context.Textbook == null)
            {
                return NotFound();
            }
            var textbook = await _context.Textbook.FindAsync(id);

            if (textbook != null)
            {
                Textbook = textbook;
                _context.Textbook.Remove(Textbook);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
