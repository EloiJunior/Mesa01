using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mesa01.Models;
using Mesa01.Services;  // para usar as classes de serviços

namespace Mesa01.Controllers
{
    public class FechamentosController : Controller
    {
        //private readonly Mesa01Context_context _context;
        private readonly FechamentoService _fechamentoService;

        public FechamentosController(FechamentoService fechamentoService /*Mesa01Context_context context*/)
        {
            _fechamentoService = fechamentoService;
        }

        // GET: Fechamentos
        public async Task<IActionResult> Index()
        {
            return View(await _fechamentoService.FindAllAsync());
        }

        // GET: Fechamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fechamento = await _fechamentoService.FindByIdAsync(id.Value);

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
                await _fechamentoService.InsertAsync(fechamento);
               
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

            var fechamento = await _fechamentoService.FindByIdAsync(id.Value);
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
                    await _fechamentoService.UpdateAsync(fechamento);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _fechamentoService.FechamentoExistsAsync(fechamento.Id))
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

            var fechamento = await _fechamentoService.FindByIdAsync(id.Value);

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
            await _fechamentoService.RemoveAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
