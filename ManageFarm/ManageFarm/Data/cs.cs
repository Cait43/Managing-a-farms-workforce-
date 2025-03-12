using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ManageFarm.Models;

namespace ManageFarm.Data.ManageFarmContext
{
    public class cs : DbContext
    {
        public cs (DbContextOptions<cs> options)
            : base(options)
        {
        }

        public DbSet<ManageFarm.Models.Staff> Staff { get; set; } = default!;
    }
}
