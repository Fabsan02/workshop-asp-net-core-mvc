using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class ProductsController : Controller //controller envia uma requisição para view solicitada pelo usuario
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private List<Category> categories;

        public ProductsController( ProductService productService,CategoryService categoryService )
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var list = _productService.FindAllAsync( );

            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.FindAllAsync ( );
            var viewModel = new ProductFormViewModel { Category = categories };//Quando a tela de  cadastro for requisitada ja vai aparecer com uma tabela populada.
            return View ( viewModel );//passa o objeto com a lista recebida de departamentos 
        }

        /////////////////////////////////
        ///
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Product product )//gera a ação de inserir(POST) no banco de dados
        {
            if (!ModelState.IsValid)//nova instanciação para validação de requisição
            {
                var departments = await _categoryService.FindAllAsync ( );
                var viewModel = new ProductFormViewModel { Product = product, Category = categories };
                return View ( viewModel );
            }
            await _productService.InsertAsync ( product );
            return RedirectToAction ( nameof ( Index ) );
        }

        public async Task<IActionResult> Delete( int? id )//recebe um item opcional usando ?, que e o Id usa
        {
            if (id == null)
            {
                return RedirectToAction ( nameof ( Error ), new { message = "Id not found" } );//estancia uma resposta basica para caso seja nullo o valor
            }
            var obj = await _productService.FindByIdAsync ( id.Value );//pega o objetoa ser deletado
            if (obj == null)
            {
                return RedirectToAction ( nameof ( Error ), new { message = "Id notprovided" } );
            }
            return View ( obj );
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete( int id )
        {
            try
            {
                await _productService.RemoveAsync ( id );
                return RedirectToAction ( nameof ( Index ) );//apos remover o registro retorna para a pagina index de vendedor.

            }
            catch (IntegrityException e)
            {
                return RedirectToAction ( nameof ( Error ), new { message = e.Message } );
            }

        }
        public async Task<IActionResult> Details( int? id )
        {
            if (id == null)
            {
                return RedirectToAction ( nameof ( Error ), new { message = "Id notprovided" } );//estancia uma resposta basica para caso seja nullo o valor
            }
            var obj = await _productService.FindByIdAsync ( id.Value );//pega o objeto a ser mostrado os detalhes
            if (obj == null)
            {
                return RedirectToAction ( nameof ( Error ), new { message = "Id not found" } ); ;
            }
            return View ( obj );
        }

        public async Task<IActionResult> Edit( int? id )
        {
            if (id == null)
            {
                return RedirectToAction ( nameof ( Error ), new { message = "Id notprovided" } );
            }
            var obj = await _productService.FindByIdAsync ( id.Value );//busca obejto no banco de dados
            if (obj == null)
            {
                return RedirectToAction ( nameof ( Error ), new { message = "Id not found" } );
            }
            List<Category> categories = await _categoryService.FindAllAsync ( );
            ProductFormViewModel viewModel = new ProductFormViewModel { Product = obj, Categories = categories};//preenche os dados no formulario com o objeto existente...
            return View ( viewModel );
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( int id, Product product )
        {
            if (!ModelState.IsValid)
            {
                var departments = await _categoryService.FindAllAsync ( );
                var viewModel = new ProductFormViewModel { Product = product, Categories = categories };
                return View ( viewModel );
            }

            if (id != product.id)
            {
                return RedirectToAction ( nameof ( Error ), new { message = "Id missmatch" } );
            }
            try
            {
                await _productService.Update ( product );//essa chamada pode gerar exceptions
                return RedirectToAction ( nameof ( Index ) );
            }
            catch (ApplicationException e)
            {
                return RedirectToAction ( nameof ( Error ), new { message = e.Message } );
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



        public IActionResult Error( string message )
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier //pega o Id interno da requisição.
            };
            return View ( viewModel );
        }

    }
}




