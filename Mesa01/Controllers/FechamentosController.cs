using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mesa01.Models;
using Mesa01.Services;  // para usar as classes de serviços
using Mesa01.Models.ViewModels;
using System.Diagnostics;  //para usar o Activity do erro

namespace Mesa01.Controllers
{
    public class FechamentosController : Controller
    {
        //private readonly Mesa01Context_context _context;
        private readonly FechamentoService _fechamentoService;
        private readonly DepartamentoService _departamentoService;  //injeção de dependencia do DepartamentoService
        private readonly OperadorService _operadorService;          //injeção de dependencia do OperadorService
        private readonly TipoService _tipoService;
        

        public FechamentosController(FechamentoService fechamentoService, DepartamentoService departamentoService, OperadorService operadorService, TipoService tipoService)
        {
            _fechamentoService = fechamentoService;
            _departamentoService = departamentoService;
            _operadorService = operadorService;
            _tipoService = tipoService;
        }

        // GET: Fechamentos
        public async Task<IActionResult> Index()
        {
            var list = await _fechamentoService.FindAllAsync();


            /*Metodo para inserir na list o nome do operador e o nome do tipo atraves dos Ids, Esse metodo não é feito pelo entitie framework, mas é melhor usar o Include no FechamentoService
            foreach(var x in list)
            {
                x.Operador = await _operadorService.FindByIdAsync(x.OperadorId);
                x.Tipo = await _tipoService.FindByIdAsync(x.TipoId);
            }
            */
           return View(list);
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
        public async Task<IActionResult> Create()
        {
            var operadores = await _operadorService.FindAllAsync(); //criei uma variavel que através do serviço DepartamentoService, busca no banco de dados todos os departamentos
            var tipos = await _tipoService.FindAllAsync(); //criei uma variavel que através do serviço DepartamentoService, busca no banco de dados todos os departamentos
            var viewModel = new FechamentoFormViewModel { Operadores = operadores , Tipos = tipos }; // agora vamos instanciar um objeto do nosso viewModel, no Departamentos vamos iniciar com a lista de departamentos que acabamos de gerar acima
            return View(viewModel);
            //return View();
        }

        // POST: Fechamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoId,Data,Empresa,Valor,Taxa,Despesa,Fluxo,Banco,OperadorId,Status")] Fechamento fechamento)
        {
            if (ModelState.IsValid)
            {
                await _fechamentoService.InsertAsync(fechamento);
               
                return RedirectToAction(nameof(Index));
            }

            //return View(fechamento);  //foi criado assim pelo framework, porem agora será pelo FechamentoFormViewModel  //se as validações não foram atendidas não é criado na tabela e devolve com o objeto incompleto sem criar na tabela
            //essa situação pode ocorrer se as validações não foram feita a nivel de JavaScript
            //criei a lista de departamentos e atraves da ViewModel FechamentoFormViewModel, consigo apresentar na tela de edição o combo de Operadores para a edição do operador
            var operadores = await _operadorService.FindAllAsync();
            var tipos = await _tipoService.FindAllAsync(); 
            var viewModel = new FechamentoFormViewModel { Operadores = operadores, Tipos = tipos }; 
            return View(viewModel);
            // agora não mais retornando o operador somente, mas o operador e a lista de departamentos, com os erros de validação

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
            //criei a lista de departamentos e atraves da ViewModel OperadorFormViewModel, consigo apresentar na tela de edição o combo de departamentos para a edição do operador
            List<Operador> operadores = await _operadorService.FindAllAsync();
            List<Tipo> tipos = await _tipoService.FindAllAsync();
            FechamentoFormViewModel viewModel = new FechamentoFormViewModel { Fechamento = fechamento, Operadores = operadores , Tipos = tipos };
            return View(viewModel); // agora não mais retornando o operador somente, mas o operador e a lista de departamentos
        }

        // POST: Fechamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoId,OperadorId,Data,Empresa,Valor,Taxa,Despesa,Fluxo,Banco")] Fechamento fechamento)
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
            //criei a lista de departamentos e atraves da ViewModel OperadorFormViewModel, consigo apresentar na tela de edição o combo de departamentos para a edição do operador
            
            var operadores = await _operadorService.FindAllAsync();
            var tipos = await _tipoService.FindAllAsync();
            var viewModel = new FechamentoFormViewModel { Fechamento = fechamento, Operadores = operadores, Tipos = tipos };
            return View(viewModel);                                                  // agora não mais retornando o operador somente, mas o operador e a lista de departamentos
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
            try
            {
                await _fechamentoService.RemoveAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
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
