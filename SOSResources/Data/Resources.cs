using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SOSResources.Models;

namespace SOSResources.Data
{
    public class Resources : DbContext
    {
        public Resources (DbContextOptions<Resources> options)
            : base(options)
        {
        }

        public DbSet<SOSResources.Models.Resource> Resource { get; set; } = default!;
    }
}
