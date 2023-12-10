using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SOSResources.Data;
using SOSResources.Models;

namespace SOSResources.Pages.ResourceTypes
{
    public class CreateModel : PageModel
    {
        private readonly SOSResources.Data.SOSContext _context;

        public CreateModel(SOSResources.Data.SOSContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ResourceType ResourceType { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ResourceTypes.Add(ResourceType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
