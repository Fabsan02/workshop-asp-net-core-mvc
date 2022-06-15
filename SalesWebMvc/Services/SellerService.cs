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

        public void Insert(Seller obj)//insere um objeto no banco de dados...
        {
            obj.Department = _context.Department.First();//associa o primeiro departamento do banco e associa ao vendedor medida temporaria.
            _context.Add(obj);//adiciona um novo vendedor no banco
            _context.SaveChanges();//salva a adição do novo vendedor(que e recebido em OBJ
        }
    }
}
