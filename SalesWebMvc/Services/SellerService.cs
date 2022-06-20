using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


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
            _context.Add(obj);//adiciona um novo vendedor no banco
            _context.SaveChanges();//salva a adição do novo vendedor(que e recebido em OBJ
        }

        public Seller FindById(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }
        //Metodo remove

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);//remove o objeto desejado
            _context.SaveChanges();//confirma a remoção do objeto do banco de dados....

        }       
    }
}
