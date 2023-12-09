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

namespace SOSResources.Pages.ResourceRequests
{
    public class EditModel : PageModel
    {
        private readonly SOSResources.Data.SOSContext _context;

        public EditModel(SOSResources.Data.SOSContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ResourceRequest ResourceRequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ResourceRequests == null)
            {
                return NotFound();
            }

            var resourcerequest =  await _context.ResourceRequests.FirstOrDefaultAsync(m => m.ID == id);
            if (resourcerequest == null)
            {
                return NotFound();
            }
            ResourceRequest = resourcerequest;
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

            _context.Attach(ResourceRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceRequestExists(ResourceRequest.ID))
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

        private bool ResourceRequestExists(int id)
        {
          return _context.ResourceRequests.Any(e => e.ID == id);
        }
    }
}
