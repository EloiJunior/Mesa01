using Mesa01.Models;
using Mesa01.Services.Exceptions;     //para usar o NotFoundException que criamos em serviços
using Microsoft.EntityFrameworkCore;  // para usar o .ToListAsync()
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mesa01.Services
{
    public class OperadorService
    {
        
        //declarar uma dependencia do serviço com o dbcontext
        //para impedir que essa dependencia não possa ser alterada
        private readonly Mesa01Context_context _context;

        //criar um construtor pra que a injeção de dependencia possa acontecer
        public OperadorService(Mesa01Context_context context)
        {
            _context = context;
        }

        // GET: Operadores
        //agora que temos a nossa dependencia do context, vamos criar uma operação FindAll para retornar uma lista com todos os operadores do banco de dados
        public async Task<List<Operador>> FindAllAsync()
        {
            return await _context.Operador.ToListAsync();  //isso vai acessar a minha tabela de dados relacionada a Operadores e me retornar em forma de lista
        }


        //Metodo Insert
        public async Task InsertAsync(Operador operador)
        {
            _context.Add(operador);
            await _context.SaveChangesAsync();
        }

        //Metodo FindById
        public async Task<Operador> FindByIdAsync(int id)
        {
            return await _context.Operador
                .Include(m => m.Departamento).FirstOrDefaultAsync(m => m.Id == id); // Include nesse caso faz um join da tabela Operador com a tabela Departamento, isso é o Eager Loading que é carregar outros objetos associados ao objeto principal
        }

        //Metodo Remove
        public async Task RemoveAsync(int id)
        {
            try
            {
                var operador = await _context.Operador.FindAsync(id);
                _context.Operador.Remove(operador);
                await _context.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                throw new IntegrityException(e.Message);
            }

        }


        //Metodo Update
        public async Task UpdateAsync(Operador operador)
        {
            if (!await _context.Operador.AnyAsync(x => x.Id == operador.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try //nesse bloco vamos tentar realizar o update na tabela
            {
                _context.Update(operador);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)    //se ocorrer uma exceção de concorrencia a nivel de acesso a bco de dados, vamos pegar esse erro
            {
                throw new DbConcurrencyException(e.Message); //e vamos apresentar a nivel de serviço a mensagem personalizada, ou nesse caso a propria mensagem que o framework trouxe
            }



        }


        //?? criado por acidente tentando escrever o NotImplementedException()??
        private object NotFound()
        {
            throw new NotImplementedException();
        }

        //Metodo Exists
        public async Task<bool> OperadorExistsAsync(int id)
        {
            return await _context.Operador.AnyAsync(e => e.Id == id);
        }



    }


}
