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
    public class DetailsModel : PageModel
    {
        private readonly SOSResources.Data.Textbooks _context;

        public DetailsModel(SOSResources.Data.Textbooks context)
        {
            _context = context;
        }

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
    }
}
