using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mesa01.Models
{
    public class Mesa01Context : DbContext
    {
        public Mesa01Context (DbContextOptions<Mesa01Context> options)
            : base(options)
        {
        }

        public DbSet<Mesa01.Models.Fechamento> Fechamento { get; set; }
    }
}
