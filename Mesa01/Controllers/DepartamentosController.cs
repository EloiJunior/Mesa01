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
    public class DepartamentosController : Controller
    {
        //private readonly Mesa01Context_context _context; //criado pelo framework, vou migrar para o DepartamentoService

        private readonly DepartamentoService _departamentoService;  //injeção de dependencia do DepartamentoService

        public DepartamentosController(/*Mesa01Context_context context*/ DepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        // GET: Departamentos
        public async Task<IActionResult> Index()
        {
            return View(await _departamentoService.FindAllAsync());
        }

        // GET: Departamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _departamentoService.FindByIdAsync(id.Value);
                
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // GET: Departamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                
                await _departamentoService.InsertAsync(departamento);
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        // GET: Departamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _departamentoService.FindByIdAsync(id.Value);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        // POST: Departamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Departamento departamento)
        {
            if (id != departamento.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _departamentoService.UpdateAsync(departamento);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _departamentoService.DepartamentoExists(departamento.Id))
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
            return View(departamento);
        }

        // GET: Departamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _departamentoService.FindByIdAsync(id.Value);

            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // POST: Departamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _departamentoService.RemoveAsync(id);
            
            return RedirectToAction(nameof(Index));
        }

    }
}
