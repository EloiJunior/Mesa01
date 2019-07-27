using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mesa01.Models;
using Mesa01.Services.Exceptions;     // para usar as exceções personalizadas
using Microsoft.EntityFrameworkCore;  // para usar o .ToListAsync()

namespace Mesa01.Services
{
    public class FechamentoService
    {
        //declarar uma dependencia do serviço com o dbcontext
        //para impedir que essa dependencia não possa ser alterada
        private readonly Mesa01Context_context _context;

        //criar um construtor pra que a injeção de dependencia possa acontecer
        public FechamentoService(Mesa01Context_context context)
        {
            _context = context;
        }

        //Metodo FindAll
        public async Task<List<Fechamento>> FindAllAsync()
        {
            return await _context.Fechamento.ToListAsync();  //isso vai acessar a minha tabela de dados relacionada a fechamento e me retornar em forma de lista
        }

        //Metodo Insert
        public async Task InsertAsync(Fechamento fechamento)
        {
            _context.Add(fechamento);
            await _context.SaveChangesAsync();
        }

        //Metodo FindById
        public async Task<Fechamento> FindByIdAsync(int id)
        {
            return await _context.Fechamento
                 .Include(m => m.Operador).Include(m => m.Tipo).FirstOrDefaultAsync(m => m.Id == id); //ponto de atenção pelo segundo include
        }

        //Metodo Remove
        public async Task RemoveAsync(int id)
        {
            try
            {
                var fechamento = await _context.Fechamento.FindAsync(id);
                _context.Fechamento.Remove(fechamento);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e)
            {
                throw new IntegrityException(e.Message);
            }

        }

        //Metodo Update
        public async Task UpdateAsync(Fechamento fechamento)
        {
            if (!await _context.Fechamento.AnyAsync(x => x.Id == fechamento.Id))
            {
                throw new NotImplementedException();
            }
            try
            {
                _context.Update(fechamento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new NotImplementedException();
            }
        }


        //?? criado por acidente tentando escrever o NotImplementedException()??
        private object NotFound()
        {
            throw new NotImplementedException();
        }

        //Metodo Exists
        public async Task<bool> FechamentoExistsAsync(int id)
        {
            return await _context.Fechamento.AnyAsync(e => e.Id == id);
        }



    }
}
