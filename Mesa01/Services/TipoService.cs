using Mesa01.Models;
using System.Collections.Generic; // para usar o list
using System.Linq;
using System.Threading.Tasks;     // para usar o Task, do processamento assyncrono
using Microsoft.EntityFrameworkCore; //para usar o ToListAsync()
using System;                        //para usar o Not Implemented Exception

namespace Mesa01.Services
{
    public class TipoService
    {
        private readonly Mesa01Context_context _context;

        public TipoService(Mesa01Context_context context)
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
        public async Task<List<Tipo>> FindAllAsync()
        {
            return await _context.Tipo.OrderBy(x => x.Nome).ToListAsync(); //OrderBy = ordenar, linq(s => x.Nome) = ordenar pelo nome
        }

        public async Task<Tipo> FindByIdAsync(int id)
        {
            return await _context.Tipo.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task InsertAsync(Tipo tipo)
        {
            _context.Tipo.Add(tipo);
            await _context.SaveChangesAsync();
        }

        //Metodo Update
        public async Task UpdateAsync(Tipo tipo)
        {
            if (!await _context.Tipo.AnyAsync(x => x.Id == tipo.Id))
            {
                throw new NotImplementedException();
            }
            try
            {
                _context.Update(tipo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new NotImplementedException();
            }
        }



        //Metodo Remove
        public async Task RemoveAsync(int id)
        {
            var operador = await _context.Tipo.FindAsync(id);
            _context.Tipo.Remove(operador);
            await _context.SaveChangesAsync();
        }

        //metodo criado pelo framework no controlador, migrei para o serviço
        public async Task<bool> TipoExists(int id)
        {
            return await _context.Tipo.AnyAsync(e => e.Id == id);
        }

    }
}
