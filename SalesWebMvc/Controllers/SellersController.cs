using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using SalesWebMvc.Models.ViewModels;
using System.Collections.Generic;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;
using System;

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
                return RedirectToAction(nameof(Error), new {message="Id not found" });//estancia uma resposta basica para caso seja nullo o valor
            }
            var obj = _sellerService.FindById(id.Value);//pega o objetoa ser deletado
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id notprovided" });
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));//apos remover o registro retorna para a pagina index de vendedor.

        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id notprovided" });//estancia uma resposta basica para caso seja nullo o valor
            }
            var obj = _sellerService.FindById(id.Value);//pega o objeto a ser mostrado os detalhes
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" }); ;
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id notprovided" });
            }
            var obj = _sellerService.FindById(id.Value);//busca obejto no banco de dados
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" }); 
            }
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };//preenche os dados no formulario com o objeto existente...
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id missmatch" });
            }
            try
            {
                _sellerService.Update(seller);//essa chamada pode gerar exceptions
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message});
            }
         //ApplicationException Atende os dois tratamentos abaixo se chama upcast

           /*    catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message});
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }*/
         }
    

       
            public IActionResult Error(string message)
            {
                var viewModel = new ErrorViewModel
                {
                    Message = message,
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier //pega o Id interno da requisição.
                };
                return View(viewModel);
            }

        }
    }
