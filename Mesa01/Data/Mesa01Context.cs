using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mesa01.Models
{
    public class Mesa01Context_context : DbContext
    {
        public Mesa01Context_context (DbContextOptions<Mesa01Context_context> options)
            : base(options)
        {
        }

        public DbSet<Mesa01.Models.Departamento> Departamento { get; set; }
        public DbSet<Mesa01.Models.Fechamento> Fechamento { get; set; }
        public DbSet<Mesa01.Models.Operador> Operador { get; set; }


    }
}
