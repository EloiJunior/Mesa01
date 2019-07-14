using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; //biblioteca para usar o comando Include, para fazer um join de 2 tabelas
using Mesa01.Models;
using Mesa01.Models.ViewModels;
using Mesa01.Services;
using Mesa01.Services.Exceptions;  //para usar o NotFoundException
using System.Diagnostics;  //para usar o Activity

namespace Mesa01.Controllers
{
    public class OperadoresController : Controller
    {
        private readonly DepartamentoService _departamentoService;  //injeção de dependencia do DepartamentoService
        private readonly OperadorService _operadorService;          //injeção de dependencia do OperadorService
        //private readonly Mesa01Context_context _context;          //criado pelo framework, migrei para o OperadorService

        //construtor que inicialmente foi criado pelo CRUD, incluimos o DepartamentoService
        public OperadoresController(/*Mesa01Context_context context,*/ OperadorService operadorService,  DepartamentoService departamentoService) // coloquei o DepartamentoService e o OperadorService do serviço criado no construtor
        {
            _operadorService = operadorService;          //_operadorService da classe, recebe operadorService do argumento

            _departamentoService = departamentoService; //_departamentoService da classe, recebe departamentoService do argumento
        }

        // GET: Operadores
        public async Task<IActionResult> Index()
        {
            return View(await _operadorService.FindAllAsync());
        }

        // GET: Operadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }


            var operador = await _operadorService.FindByIdAsync(id.Value);

            if (operador == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(operador);
        }

        // GET: Operadores/Create
        public async Task<IActionResult> Create()
        {
            var departamentos = await _departamentoService.FindAllAsync(); //criei uma variavel que através do serviço DepartamentoService, busca no banco de dados todos os departamentos
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
            if (ModelState.IsValid)                       //teste criado para checar se as validações do Model estão atendidas, elas podem vir erradas se as validações não foram feitas a nivel de JavaScript
            {
                await _operadorService.InsertAsync(operador);
                return RedirectToAction(nameof(Index));
            }
            //return View(operador);  //foi criado assim pelo framework, porem agora será pelo operadorFormViewModel  //se as validações não foram atendidas não é criado na tabela e devolve com o objeto incompleto sem criar na tabela
                                                                                //essa situação pode ocorrer se as validações não foram feita a nivel de JavaScript
             //criei a lista de departamentos e atraves da ViewModel OperadorFormViewModel, consigo apresentar na tela de edição o combo de departamentos para a edição do operador
            var departamentos = await _departamentoService.FindAllAsync();
            var viewModel = new OperadorFormViewModel { Operador = operador, Departamentos = departamentos };
            return View(viewModel);                      // agora não mais retornando o operador somente, mas o operador e a lista de departamentos, com os erros de validação
        }

        // GET: Operadores/Edit/5
        public async Task<IActionResult> Edit(int? id) //esse opcional "?" foi colocado somente para evitar erro de execução, na verdade o id é obrigatorio
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var operador = await _operadorService.FindByIdAsync(id.Value); //essa variavel operador, vai ser usada no OperadorFormViewModel abaixo:

            if (operador == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            //criei a lista de departamentos e atraves da ViewModel OperadorFormViewModel, consigo apresentar na tela de edição o combo de departamentos para a edição do operador
            List<Departamento> departamentos = await _departamentoService.FindAllAsync();
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
                //return BadRequest();
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _operadorService.UpdateAsync(operador);
                   
                }
                catch (ApplicationException e)
                {
                    if (!await _operadorService.OperadorExistsAsync(operador.Id))
                    {
                        //return NotFound();
                        return RedirectToAction(nameof(Error), new { message = "Id not found" });
                    }
                    else
                    {
                        //throw new DbConcurrencyException(e.Message);
                        return RedirectToAction(nameof(Error), new { message = e.Message });
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //criei a lista de departamentos e atraves da ViewModel OperadorFormViewModel, consigo apresentar na tela de edição o combo de departamentos para a edição do operador
            var departamentos = await _departamentoService.FindAllAsync();
            var viewModel = new OperadorFormViewModel { Operador = operador, Departamentos = departamentos };
            return View(viewModel);                                                  // agora não mais retornando o operador somente, mas o operador e a lista de departamentos
        }

        // GET: Operadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                // return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var operador = await _operadorService.FindByIdAsync(id.Value);
                
            if (operador == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(operador);
        }

        // POST: Operadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _operadorService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }


        //Ação para pegar o Erro e devolver o Erro personalizado para a View
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,

                //Macete do framework para pegar o id interno da requisição
                //        Current opcional "?"   
                //se for nulo vamos usar o operador de coalescencia nula "??"
                //e vamos dizer então que queremos no lugar o objeto Http...:
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }

    }
}
