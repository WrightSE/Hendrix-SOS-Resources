using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SOSResources.Models;

namespace SOSResources.Data
{
    public class Textbooks : DbContext
    {
        public Textbooks (DbContextOptions<Textbooks> options)
            : base(options)
        {
        }

        public DbSet<SOSResources.Models.Textbook> Textbook { get; set; } = default!;
    }
}
