using Mesa01.Models; //para usar a dependencia do context
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // para usar o Include

namespace Mesa01.Services
{
    public class SalesRecordService
    {
        //criamos a denpendencia do context:
        private readonly Mesa01Context_context _context;

        public SalesRecordService(Mesa01Context_context context)
        {
            _context = context;
        }

        //Metodo FindBydate
        public async Task<List<Fechamento>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.Fechamento select obj; //obs. trasforma uma busca do tipo DbSet em um objeto IQueryable: que é aquele objeto que podemos construir as consultas nele
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Data >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Data <= maxDate.Value);
            }
            //com o return abaixo já mostraria a lista, porem vamos melhorar esse retorno:
            //return result.ToList();
            return await result
                .Include(x => x.Operador)               //join com a tabela de operador
                .Include(x => x.Operador.Departamento)  //join com o departamento do operador
                .OrderByDescending(x => x.Data)         //ordenar descendente pela data
                .ToListAsync();
        }

        //Metodo FindBydateGrouping
        public async Task<List<IGrouping<Departamento, Fechamento>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.Fechamento select obj; //obs. trasforma uma busca do tipo DbSet em um objeto IQueryable: que é aquele objeto que podemos construir as consultas nele
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Data >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Data <= maxDate.Value);
            }
            //com o return abaixo já mostraria a lista, porem vamos melhorar esse retorno:
            //return result.ToList();
            return await result
                .Include(x => x.Operador)               //join com a tabela de operador
                .Include(x => x.Operador.Departamento)  //join com o departamento do operador
                .OrderByDescending(x => x.Data)         //ordenar descendente pela data
                .GroupBy(x => x.Operador.Departamento)  //agrupar por departamento,e para isso a list<fechamento> vira list<Igrouping<departamento,fechamento>>
                .ToListAsync();
        }

    }
}
