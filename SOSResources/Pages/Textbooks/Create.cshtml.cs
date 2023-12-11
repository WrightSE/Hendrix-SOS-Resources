using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using SOSResources.Data;
using SOSResources.Models;

namespace SOSResources.Pages.Textbooks
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
        public Textbook Textbook { get; set; }
        

        public int Copies { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int copies)
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            //Check if the textbook already exists and only create it if not.
            var matches = _context.Textbooks.Where(t => t.Title.ToUpper().Equals(Textbook.Title.ToUpper()) && t.Author.ToUpper().Equals(Textbook.Author.ToUpper()));
            Textbook tb = null;
            if (!matches.IsNullOrEmpty()){
                if (Textbook.Edition.IsNullOrEmpty()){
                    var editionMatch = matches.Where(t => String.IsNullOrWhiteSpace(t.Edition));
                    if (!editionMatch.IsNullOrEmpty()){
                        tb = editionMatch.ToArray()[0];
                    }
                } else {
                    var editionMatch = matches.Where(t => t.Edition.Equals(Textbook.Edition));
                    if (!editionMatch.IsNullOrEmpty()){
                        tb = editionMatch.ToArray()[0];
                    }
                }
            }

            if (tb == null){
                _context.Textbooks.Add(Textbook);
            } else {
                Textbook = tb;
            }

            
            //int.TryParse(Request.Form["copies"].ToString(), out int copies);

            for (int i = 0; i < copies; i++) {
                Copy c = new Copy{
                    textbook = Textbook,
                    CheckedOut = false
                };
                _context.Copies.Add(c);
            };

            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}
