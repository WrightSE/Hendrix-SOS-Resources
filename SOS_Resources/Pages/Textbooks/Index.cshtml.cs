using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SOS_Resources.Data;
using SOS_Resources.Models;

namespace SOS_Resources.Pages.Textbooks
{
    public class IndexModel : PageModel
    {
        private readonly SOS_Resources.Data.ApplicationDbContext _context;

        public IndexModel(SOS_Resources.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public string TitleSort { get; set; }
        public string AuthorSort { get; set; }
        public string TitleFilter { get; set; }
        public string AuthorFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Textbook> Textbooks { get; set; }

        public async Task OnGetAsync(string sortOrder, string titleSearchString, string authorSearchString, string titleFilter, string authorFilter, int? pageIndex, int pageSize = 10)
        {
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "Author" ? "author_desc" : "Author";
            if (titleSearchString != null || authorSearchString != null)
            {
                pageIndex = 1;
            }
            titleSearchString = titleSearchString != null ? titleSearchString : titleFilter;
            authorSearchString = authorSearchString != null ? authorSearchString : authorFilter;

            TitleFilter = titleSearchString;
            AuthorFilter = authorSearchString;
            
            IQueryable<Textbook> TextbookIQ = from tb in _context.Textbooks
                                            select tb;
            if (!String.IsNullOrEmpty(authorSearchString) && !String.IsNullOrWhiteSpace(titleSearchString))
            {
                TextbookIQ = TextbookIQ.Where(tb => tb.Author.Contains(authorSearchString)
                                && tb.Title.Contains(titleSearchString));
            } else if (!String.IsNullOrWhiteSpace(titleSearchString)){
                TextbookIQ = TextbookIQ.Where(tb => tb.Title.Contains(titleSearchString));
            } else if (!String.IsNullOrWhiteSpace(authorSearchString)){
                TextbookIQ = TextbookIQ.Where(tb => tb.Author.Contains(authorSearchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    TextbookIQ = TextbookIQ.OrderByDescending(tb => tb.Title);
                    break;
                case "Author":
                    TextbookIQ = TextbookIQ.OrderBy(tb => tb.Author);
                    break;
                case "author_desc":
                    TextbookIQ = TextbookIQ.OrderByDescending(tb => tb.Author);
                    break;
                default:
                    TextbookIQ = TextbookIQ.OrderBy(tb => tb.Title);
                    break;
            }

            if (_context.Textbooks != null)
            {
                Textbooks = await PaginatedList<Textbook>.CreateAsync(TextbookIQ.AsNoTracking().Include(t => t.Copies), pageIndex ?? 1, pageSize);
            }
        }
    }
}
