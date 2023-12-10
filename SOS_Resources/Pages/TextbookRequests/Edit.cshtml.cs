using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SOS_Resources.Data;
using SOS_Resources.Models;

namespace SOS_Resources.Pages.TextbookRequests
{
    public class EditModel : PageModel
    {
        private readonly SOS_Resources.Data.ApplicationDbContext _context;

        public EditModel(SOS_Resources.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TextbookRequest TextbookRequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TextbookRequests == null)
            {
                return NotFound();
            }

            var textbookrequest =  await _context.TextbookRequests.FirstOrDefaultAsync(m => m.ID == id);
            if (textbookrequest == null)
            {
                return NotFound();
            }
            TextbookRequest = textbookrequest;
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

            _context.Attach(TextbookRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TextbookRequestExists(TextbookRequest.ID))
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

        private bool TextbookRequestExists(int id)
        {
          return _context.TextbookRequests.Any(e => e.ID == id);
        }
    }
}
