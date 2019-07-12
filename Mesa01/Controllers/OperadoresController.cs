using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mesa01.Models;
using Mesa01.Models.ViewModels;
using Mesa01.Services;
using Microsoft.EntityFrameworkCore; //biblioteca para usar o comando Include, para fazer um join de 2 tabelas



namespace Mesa01.Controllers
{
    public class OperadoresController : Controller
    {
        private readonly DepartamentoService _departamentoService;

        private readonly Mesa01Context_context _context;

        //construtor que inicialmente foi criado pelo CRUD, inclui o DepartamentoService
        public OperadoresController(Mesa01Context_context context, DepartamentoService departamentoService) // coloquei o DepartamentoService do serviço criado no construtor
        {
            _context = context;

            _departamentoService = departamentoService; //_departamentoService da classe, recebe departamentoService do argumento
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
                .Include(m => m.Departamento).FirstOrDefaultAsync(m => m.Id == id); // Include nesse caso faz um join da tabela Operador com a tabela Departamento, isso é o Eager Loading que é carregar outros objetos associados ao objeto principal

            if (operador == null)
            {
                return NotFound();
            }

            return View(operador);
        }

        // GET: Operadores/Create
        public IActionResult Create()
        {
            var departamentos = _departamentoService.FindAll(); //criei uma variavel que através do serviço DepartamentoService, busca no banco de dados todos os departamentos
            var viewModel = new OperadorFormViewModel { Departamentos = departamentos }; // agora vamos instanciar um objeto do nosso viewModel, no Departamentos vamos iniciar com a lista de departamentos que acabamos de gerar acima
            return View(viewModel);
        }

        // POST: Operadores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,BirthDate,BaseSalary,DepartamentoId")] Operador operador)
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
            //criei a lista de departamentos e atraves da ViewModel OperadorFormViewModel, consigo apresentar na tela de edição o combo de departamentos para a edição do operador
            List<Departamento> departamentos = _departamentoService.FindAll();
            OperadorFormViewModel viewModel = new OperadorFormViewModel { Operador = operador, Departamentos = departamentos };
            return View(viewModel); // agora não mais retornando o operador somente, mas o operador e a lista de departamentos
        }

        // POST: Operadores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,BirthDate,BaseSalary,DepartamentoId")] Operador operador)
        {
            if (id != operador.Id)
            {
                return BadRequest();
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
