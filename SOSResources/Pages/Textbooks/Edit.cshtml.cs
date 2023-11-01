using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SOSResources.Data;
using SOSResources.Models;

namespace SOSResources.Pages.Textbooks
{
    public class EditModel : PageModel
    {
        private readonly SOSResources.Data.Textbooks _context;

        public EditModel(SOSResources.Data.Textbooks context)
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

            var textbook =  await _context.Textbook.FirstOrDefaultAsync(m => m.ID == id);
            if (textbook == null)
            {
                return NotFound();
            }
            Textbook = textbook;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Textbook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TextbookExists(Textbook.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TextbookExists(int id)
        {
          return (_context.Textbook?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
