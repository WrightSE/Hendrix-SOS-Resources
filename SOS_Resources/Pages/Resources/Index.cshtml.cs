using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NuGet.Protocol.Core.Types;
using SOS_Resources.Data;
using SOS_Resources.Models;

namespace SOS_Resources.Pages.Resources
{
    public class IndexModel : TypeNamePageModel
    {
        private readonly SOS_Resources.Data.ApplicationDbContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public string NameSort { get; set; }
        public string TypeSort { get; set; }
        public string NameFilter { get; set; }
        public string TypeFilter { get; set; }
        public string CurrentSort { get; set; }


        
        public PaginatedList<Resource> Resources { get;set; } = default!;


        

        public async Task OnGetAsync(string sortOrder, string searchString, string typeString, int? pageIndex, int pageSize = 10)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            TypeSort = sortOrder == "Type" ? "type_desc" : "Type";
            NameFilter = searchString;
            TypeFilter = typeString;

            PopulateTypesDropdownList(_context);



            IQueryable<Resource> resourcesIQ = from r in _context.Resources
                                                    select r;

            
            if (!String.IsNullOrEmpty(searchString))
            {
                resourcesIQ = resourcesIQ.Where(r => r.Name.ToUpper().Contains(searchString.ToUpper()));
                pageIndex = 1;
                                
            } if (!String.IsNullOrEmpty(typeString)){
                resourcesIQ = resourcesIQ.Where(r => r.Type.ID == int.Parse(typeString));
                pageIndex = 1;
            }
            switch(sortOrder){
                case"name_desc":
                    resourcesIQ = resourcesIQ.OrderByDescending(r => r.Name);
                    break;
                case"Type":
                resourcesIQ = resourcesIQ.OrderBy(r => r.Type.Name);
                break;
                case"type_desc":
                    resourcesIQ = resourcesIQ.OrderByDescending(r => r.Type.Name);
                    break;
                default:
                    resourcesIQ = resourcesIQ.OrderBy(r => r.Name);
                    break;
            }
            
            Resources = await PaginatedList<Resource>.CreateAsync(
                resourcesIQ
                .Include(r => r.Type)
                .AsNoTracking(), pageIndex ?? 1, pageSize);
            
        }
    }
}
