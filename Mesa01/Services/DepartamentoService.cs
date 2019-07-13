using Mesa01.Models;
using System.Collections.Generic; // para usar o list
using System.Linq;
using System.Threading.Tasks;     // para usar o Task, do processamento assyncrono
using Microsoft.EntityFrameworkCore; //para usar o ToListAsync()

namespace Mesa01.Services
{
    public class DepartamentoService
    {
        private readonly Mesa01Context_context _context;

        public DepartamentoService(Mesa01Context_context context)
        {
            _context = context;
        }

        /*esse bloco é de processamento Syncrono, abaixo vamos alterar para processamento Assyncrono:
        public List<Departamento> FindAll()
        {
            return _context.Departamento.OrderBy(x => x.Nome).ToList(); //OrderBy = ordenar, linq(s => x.Nome) = ordenar pelo nome
        }
        */

        //vamos fazer o mesmo do bloco comentado acima, mas transformando para processamento assyncrono usando Tasks: que é um objeto que encapsula o processamento assyncrono
        public async Task<List<Departamento>> FindAllAsync()
        {
            return await _context.Departamento.OrderBy(x => x.Nome).ToListAsync(); //OrderBy = ordenar, linq(s => x.Nome) = ordenar pelo nome
        }



    }
}
