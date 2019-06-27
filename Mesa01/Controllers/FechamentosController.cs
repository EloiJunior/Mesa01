using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mesa01.Models;

namespace Mesa01.Controllers
{
    public class FechamentosController : Controller
    {
        private readonly Mesa01Context _context;

        public FechamentosController(Mesa01Context context)
        {
            _context = context;
        }

        // GET: Fechamentos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fechamento.ToListAsync());
        }

        // GET: Fechamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fechamento = await _context.Fechamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fechamento == null)
            {
                return NotFound();
            }

            return View(fechamento);
        }

        // GET: Fechamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fechamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipo,Operador,Data,Empresa,Valor,Taxa,Despesa,Fluxo,Banco")] Fechamento fechamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fechamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fechamento);
        }

        // GET: Fechamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fechamento = await _context.Fechamento.FindAsync(id);
            if (fechamento == null)
            {
                return NotFound();
            }
            return View(fechamento);
        }

        // POST: Fechamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo,Operador,Data,Empresa,Valor,Taxa,Despesa,Fluxo,Banco")] Fechamento fechamento)
        {
            if (id != fechamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fechamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FechamentoExists(fechamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fechamento);
        }

        // GET: Fechamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fechamento = await _context.Fechamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fechamento == null)
            {
                return NotFound();
            }

            return View(fechamento);
        }

        // POST: Fechamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fechamento = await _context.Fechamento.FindAsync(id);
            _context.Fechamento.Remove(fechamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FechamentoExists(int id)
        {
            return _context.Fechamento.Any(e => e.Id == id);
        }
    }
}
