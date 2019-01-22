using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace resort_backend.Models
{
    public class AccommodationContext: DbContext
    {
        public AccommodationContext(DbContextOptions<AccommodationContext> options)
            :base(options)
        {

        }
        public DbSet<AccommodationItem> AccommodationItems { get; set; }

    }
}
