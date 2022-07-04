using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc . Models;

namespace SalesWebMvc.Controllers
{
    public class CategoriasController : Controller
    {
        public IActionResult Index()
        {
            List<Categorias> list = new List<Categorias>();
            list . Add ( new Categorias { Id = 1 , Nome = "Bolsas" } );
            list . Add ( new Categorias { Id = 2 , Nome = "Cosmeticos" } );
            return View(list);
        }
    }
}