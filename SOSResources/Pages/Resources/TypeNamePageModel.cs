using SOSResources.Data;
using SOSResources.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SOSResources.Pages.Resources
{
    public class TypeNamePageModel : PageModel
    {
        public SelectList TypeNameSL { get; set; }

        public void PopulateTypesDropdownList(SOSContext _context,
            object selectedType = null)
        {
            var typesQuery = from t in _context.ResourceTypes
                             orderby t.Name
                             select t;

            TypeNameSL = new SelectList(typesQuery.AsNoTracking(),
                nameof(ResourceType.ID),
                nameof(ResourceType.Name),
                selectedType);
        }
    }

}