using Mesa01.Models;
using System.Collections.Generic; // para usar o list
using System.Linq;


namespace Mesa01.Services
{
    public class DepartamentoService
    {
        private readonly Mesa01Context_context _context;

        public DepartamentoService(Mesa01Context_context context)
        {
            _context = context;
        }

        public List<Departamento> FindAll()
        {
            return _context.Departamento.OrderBy(x => x.Nome).ToList(); //OrderBy = ordenar, linq(s => x.Nome) = ordenar pelo nome
        }

    }
}
