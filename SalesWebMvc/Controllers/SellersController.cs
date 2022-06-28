using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using SalesWebMvc.Models.ViewModels;
using System.Collections.Generic;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;
using System;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()//controlador acessa o model pega o dado na list e encaminha os dados para a view
        {
            var list = await _sellerService.FindAllAsync();//retorna uma lista de seller = vendedores

            return View(list);
        }

        public  async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };//Quando a tela de  cadastro for requisitada ja vai aparecer com uma tabela populada.
            return View(viewModel);//passa o objeto com a lista recebida de departamentos 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)//gera a ação de inserir(POST) no banco de dados
        {
            if (!ModelState.IsValid)//nova instanciação para validação de requisição
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
           await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)//recebe um item opcional usando ?, que e o Id usa
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new {message="Id not found" });//estancia uma resposta basica para caso seja nullo o valor
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);//pega o objetoa ser deletado
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id notprovided" });
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
             await _sellerService.RemoveAsync(id);
             return RedirectToAction(nameof(Index));//apos remover o registro retorna para a pagina index de vendedor.

            }
            catch(IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
             
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id notprovided" });//estancia uma resposta basica para caso seja nullo o valor
            }
            var obj =await _sellerService.FindByIdAsync(id.Value);//pega o objeto a ser mostrado os detalhes
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" }); ;
            }
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id notprovided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);//busca obejto no banco de dados
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" }); 
            }
            List<Department> departments =await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };//preenche os dados no formulario com o objeto existente...
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id missmatch" });
            }
            try
            {
               await _sellerService.Update(seller);//essa chamada pode gerar exceptions
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
