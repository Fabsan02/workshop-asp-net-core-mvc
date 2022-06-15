﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)//construtor
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()//controlador acessa o model pega o dado na list e encaminha os dados para a view
        {
            var list = _sellerService.FindAll();//retorna uma lista de seller = vendedores

            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)//gera a ação de inserir(POST) no banco de dados
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}