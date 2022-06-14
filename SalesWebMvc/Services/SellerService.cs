using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services //registrar este serviço no
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;//criar uma dependecia para acessar o DbContext


        //criar um construtor para que a injeção de dependencia possa ocorrer

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }



        public List<Seller>FindAll() //implementar uma operação no entity framework
        {
            return _context.Seller.ToList();//acessa a tabela de vendedores e convert em uma lista
        }

    }
}
