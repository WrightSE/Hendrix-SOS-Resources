using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Markup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using SOS_Resources.Data;
using SOS_Resources.Models;

namespace SOS_Resources.Pages.Utilities
{
    public class ImportModel : PageModel
    {
        private readonly SOS_Resources.Data.ApplicationDbContext _context;

        public ImportModel(SOS_Resources.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public Textbook[] Textbooks { get; set; }
        
        public string CSVString { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string inputCSVString)
        {
            if (String.IsNullOrEmpty(inputCSVString)){
                return Page();
            } else{
                CSVString = inputCSVString;
            }

            string[] lines = CSVString.Split("\n");
            if (lines[0].Contains("Title,Author")){
                lines = lines[1..^0];
            }
            Console.WriteLine(lines[0]);

            Dictionary<(string, string, string, bool), int> textbookCopies = new Dictionary<(string, string, string, bool), int>();

            //parse lines and convert duplicate textbooks to copies
            foreach (string l in lines){
                var vals = l.Split(",");
                List<string> parsed = new List<string>();
                bool inString = false;
                string currentString = "";
                foreach (string v in vals){
                    if (v.StartsWith('\"')){
                        inString = true;
                        currentString = v[1..];
                    } else if (inString){
                        currentString = String.Join(",", currentString, v);
                        if (v.EndsWith('\"')){
                            inString=false;
                            currentString = currentString[..^1];
                        }
                    } else {
                        currentString = v;
                    }
                    if (!inString){
                        parsed.Add(currentString);
                    }
                }
                if (!parsed.IsNullOrEmpty()){
                    string title = parsed[0];
                    string author = parsed[1];
                    string edition = parsed[2];
                    bool checkedOut = parsed[3] == "YES" ? true : false;

                    var tbVals = (title, author, edition, checkedOut);

                    if (textbookCopies.ContainsKey(tbVals)){
                        textbookCopies[tbVals] = textbookCopies[tbVals] + 1;
                    } else {
                        textbookCopies.Add(tbVals, 1);
                    }
                }
            }

            // create textbooks and copies from dictionary
            foreach((string, string, string, bool) k in textbookCopies.Keys){
                Textbook tb = null;
                (string title, string author, string edition, bool checkedOut) = k;
                
                var matches = _context.Textbooks.Where(t => t.Title.ToUpper().Equals(title.ToUpper()) && t.Author.ToUpper().Equals(author.ToUpper()));
                if (!matches.IsNullOrEmpty()){
                    Console.WriteLine(matches.Count());
                    if (String.IsNullOrWhiteSpace(edition)){
                        var editionMatch = matches.Where(t => String.IsNullOrWhiteSpace(t.Edition));
                        if (!editionMatch.IsNullOrEmpty()){
                            tb = editionMatch.ToArray()[0];
                        }
                    } else {
                        var editionMatch = matches.Where(t => t.Edition.Equals(edition));
                        if (!editionMatch.IsNullOrEmpty()){
                            tb = editionMatch.ToArray()[0];
                        }
                    }
                    
                }
                if (tb == null){
                    tb = new Textbook{
                        Title = title,
                        Author = author
                    };
                    if (!String.IsNullOrWhiteSpace(edition)){
                        tb.Edition = edition;
                    }
                    _context.Textbooks.Add(tb);
                }

                for (int i = 0; i < textbookCopies[k]; i++) {
                    Copy c = new Copy{
                        textbook = tb,
                        CheckedOut = checkedOut
                    };
                    _context.Copies.Add(c);
                };

            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/Textbooks/Index");
        }
    }
}
