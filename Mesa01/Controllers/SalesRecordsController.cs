﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mesa01.Services;  //para usar os serviços

namespace Mesa01.Controllers
{
    public class SalesRecordsController : Controller
    {
        //injeção de dependencia do serviço
        private readonly SalesRecordService _salesRecordService;
        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }


        public IActionResult Index()
        {
            return View();
        }


        //Tela SimpleSearch, GET
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)  //para aparecer o inicio do ano no filtro de seleçao da tela, qdo não for preenchido manualmente
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)  //para aparecer a data atual no filtro de seleção da tela, qdo não for preenchido manualmente
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");  //para aparecer no filtro a data minima
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");  //para aparecer no filtro a data maxima
            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }


        public IActionResult GroupingSearch()
        {
            return View();
        }

    }
}