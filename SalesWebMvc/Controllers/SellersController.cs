using System;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService,DepartmentService departmentService)//construtor
        {
            _sellerService = sellerService;
            _departmentService = departmentService;

        }

        public IActionResult Index()//controlador acessa o model pega o dado na list e encaminha os dados para a view
        {
            var list = _sellerService.FindAll();//retorna uma lista de seller = vendedores

            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };//Quando a tela de  cadastro for requisitada ja vai aparecer com uma tabela populada.
            return View(viewModel);//passa o objeto com a lista recebida de departamentos 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)//gera a ação de inserir(POST) no banco de dados
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)//recebe um item opcional usando ?, que e o Id usa
        {
            if(id == null)
            {
                return NotFound();//estancia uma resposta basica para caso seja nullo o valor
            }
            var obj = _sellerService.FindById(id.Value);//pega o objetoa ser deletado

            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));//apos remover o registro retorna para a pagina index de vendedor.
        }
    }
}