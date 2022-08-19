using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SalesWebMvc.Controllers
{
    public class ProductsController : Controller //controller envia uma requisição para view solicitada pelo usuario
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}