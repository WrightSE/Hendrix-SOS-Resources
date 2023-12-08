using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SOSResources.Data;
using SOSResources.Models;

namespace SOSResources.Pages.Resources
{
    public class IndexModel : PageModel
    {
        private readonly SOSResources.Data.SOSContext _context;

        public IndexModel(SOSResources.Data.SOSContext context)
        {
            _context = context;
        }
        public string NameSort { get; set; }
        public string TypeSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }


        
        public IList<Resource> Resources { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder)
        {
            
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            TypeSort = sortOrder == "Type" ? "type_desc" : "Type";

            IQueryable<Resource> resourcesIQ = from r in _context.Resources
                                                    select r;
            switch(sortOrder){
                case"name_desc":
                    resourcesIQ = resourcesIQ.OrderByDescending(r => r.Name);
                    break;
                case"Type":
                resourcesIQ = resourcesIQ.OrderBy(r => r.Type);
                break;
                case"type_desc":
                    resourcesIQ = resourcesIQ.OrderByDescending(r => r.Type);
                    break;
                default:
                    resourcesIQ = resourcesIQ.OrderBy(r => r.Name);
                    break;
            }
            

                Resources = await resourcesIQ.AsNoTracking().ToListAsync();
            
        }
    }
}
