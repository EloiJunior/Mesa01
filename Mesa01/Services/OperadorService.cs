using Mesa01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mesa01.Services
{
    public class OperadorService
    {
        /*esse bloco é da aula de serviços, mas no CRUD já criou o serviço no controller, vou manter o serviço no controler
        //declarar uma dependencia do serviço com o dbcontext
        //para impedir que essa dependencia não possa ser alterada
        private readonly Mesa01Context_context _context;

        //criar um construtor pra que a injeção de dependencia possa acontecer
        public OperadorService(Mesa01Context_context context)
        {
            _context = context;
        }

        //agora que temos a nossa dependencia do context, vamos criar uma operação FindAll para retornar uma lista com todos os operadores do banco de dados

        public List<Operador> FindAll()
        {
            return _context.Operador.ToList();  //isso vai acessar a minha tabela de dados relacionada a Operadores e me retornar em forma de lista
        }

        */
    }

    
}
