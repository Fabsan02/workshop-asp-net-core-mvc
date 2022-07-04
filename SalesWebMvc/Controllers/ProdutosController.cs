using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc . Models;

namespace SalesWebMvc.Controllers
{
    public class ProdutosController : Controller
    {
        public IActionResult Index()
        {

            List<Produtos> list = new List<Produtos> ();
            list . Add ( new Produtos { Id = 1 , Nome = "Bolsa" , Descricao = "Cor rosa com alça Longa" ,Preco = 350.00,Ativo = true } );
            list . Add ( new Produtos { Id = 2 , Nome = "Bolsa Colcci" , Descricao = "Cor marrom de mão" ,Preco = 350.00 , Ativo = true } );
            return View(list);
        }
    }
}