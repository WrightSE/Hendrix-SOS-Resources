using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SOSResources.Data;
using SOSResources.Models;

namespace SOSResources.Pages.ResourceTypes
{
    public class DeleteModel : PageModel
    {
        private readonly SOSResources.Data.SOSContext _context;

        public DeleteModel(SOSResources.Data.SOSContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ResourceType ResourceType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ResourceTypes == null)
            {
                return NotFound();
            }

            var resourcetype = await _context.ResourceTypes.FirstOrDefaultAsync(m => m.ID == id);

            if (resourcetype == null)
            {
                return NotFound();
            }
            else 
            {
                ResourceType = resourcetype;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ResourceTypes == null)
            {
                return NotFound();
            }
            var resourcetype = await _context.ResourceTypes.FindAsync(id);

            if (resourcetype != null)
            {
                ResourceType = resourcetype;
                _context.ResourceTypes.Remove(ResourceType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
