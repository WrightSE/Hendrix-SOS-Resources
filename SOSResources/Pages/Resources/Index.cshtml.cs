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
        public string NameFilter { get; set; }
        public string TypeFilter { get; set; }
        public string CurrentSort { get; set; }


        
        public IList<Resource> Resources { get;set; } = default!;


        public List<SelectListItem> TypesList { get; } = new List<SelectListItem>
        {
            new SelectListItem { Text = ""},
            new SelectListItem { Text = "First Aid Supplies"},
            new SelectListItem { Text = "Hygiene Supplies"},
            new SelectListItem { Text = "Over-the-counter Medications"},
            new SelectListItem { Text = "Personal Care Supplies"},
            new SelectListItem { Text = "Other"},
            
        };

        public async Task OnGetAsync(string sortOrder, string searchString, string typeString)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            TypeSort = sortOrder == "Type" ? "type_desc" : "Type";

            NameFilter = searchString;
            TypeFilter = typeString;



            IQueryable<Resource> resourcesIQ = from r in _context.Resources
                                                    select r;

            
            if (!String.IsNullOrEmpty(searchString))
            {
                resourcesIQ = resourcesIQ.Where(r => r.Name.ToUpper().Contains(searchString.ToUpper()));
                                
            } if (!String.IsNullOrEmpty(typeString)){
                resourcesIQ = resourcesIQ.Where(r => r.Type.Contains(typeString));
            }
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
