using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SOSResources.Data;
using SOSResources.Models;

namespace SOSResources.Pages.Textbooks
{
    public class CreateModel : PageModel
    {
        private readonly SOSResources.Data.Textbooks _context;

        public CreateModel(SOSResources.Data.Textbooks context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Textbook Textbook { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Textbook == null || Textbook == null)
            {
                return Page();
            }

            _context.Textbook.Add(Textbook);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
