using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SOS_Resources.Data;
using SOS_Resources.Models;

namespace SOS_Resources.Pages.Resources
{
    public class CreateModel : TypeNamePageModel
    {
        private readonly SOS_Resources.Data.ApplicationDbContext _context;

        public CreateModel(SOS_Resources.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateTypesDropdownList(_context);
            return Page();
        }

        [BindProperty]
        public Resource Resource { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Resource.Type = _context.ResourceTypes.Find(Resource.TypeID);
            try {
                _context.Resources.Add(Resource);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            } catch {

            }
            PopulateTypesDropdownList(_context);
            return Page();
        }
    }
}
