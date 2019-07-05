﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mesa01.Models;

namespace Mesa01.Controllers
{
    public class OperadoresController : Controller
    {
        private readonly Mesa01Context_context _context;

        public OperadoresController(Mesa01Context_context context)
        {
            _context = context;
        }

        // GET: Operadores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Operador.ToListAsync());
        }

        // GET: Operadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operador = await _context.Operador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operador == null)
            {
                return NotFound();
            }

            return View(operador);
        }

        // GET: Operadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Operadores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,BirthDate,BaseSalary")] Operador operador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(operador);
        }

        // GET: Operadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operador = await _context.Operador.FindAsync(id);
            if (operador == null)
            {
                return NotFound();
            }
            return View(operador);
        }

        // POST: Operadores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,BirthDate,BaseSalary")] Operador operador)
        {
            if (id != operador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperadorExists(operador.Id))
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
            return View(operador);
        }

        // GET: Operadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operador = await _context.Operador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operador == null)
            {
                return NotFound();
            }

            return View(operador);
        }

        // POST: Operadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operador = await _context.Operador.FindAsync(id);
            _context.Operador.Remove(operador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperadorExists(int id)
        {
            return _context.Operador.Any(e => e.Id == id);
        }
    }
}