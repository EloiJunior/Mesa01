using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; //biblioteca para usar o comando Include, para fazer um join de 2 tabelas
using Mesa01.Models;
using Mesa01.Services;  //para usar o DepartamentoService

namespace Mesa01.Controllers
{
    public class TiposController : Controller
    {
        //private readonly Mesa01Context_context _context; //criado pelo framework, vou migrar para o TipoService
        private readonly TipoService _tipoService;  //injeção de dependencia do TipoService

        //construtor
        public TiposController(/*Mesa01Context_context context*/ TipoService tipoService)
        {
            _tipoService = tipoService;
        }

        // GET: Tipos
        public async Task<IActionResult> Index()
        {
            return View(await _tipoService.FindAllAsync());
        }

        // GET: Departamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo = await _tipoService.FindByIdAsync(id.Value);

            if (tipo == null)
            {
                return NotFound();
            }

            return View(tipo);
        }

        // GET: Departamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tipos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Tipo tipo)
        {
            if (ModelState.IsValid)
            {

                await _tipoService.InsertAsync(tipo);
                return RedirectToAction(nameof(Index));
            }
            return View(tipo);
        }

        // GET: Departamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo = await _tipoService.FindByIdAsync(id.Value);
            if (tipo == null)
            {
                return NotFound();
            }
            return View(tipo);
        }

        // POST: Tipos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Tipo tipo)
        {
            if (id != tipo.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _tipoService.UpdateAsync(tipo);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _tipoService.TipoExists(tipo.Id))
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
            return View(tipo);
        }

        // GET: Departamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo = await _tipoService.FindByIdAsync(id.Value);

            if (tipo == null)
            {
                return NotFound();
            }

            return View(tipo);
        }

        // POST: Departamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tipoService.RemoveAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
