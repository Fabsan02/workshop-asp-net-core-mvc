using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

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
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);//carrega o vendedor por padrao somente com _context.Seller.FirstOrDefault(obj => obj.Id == id)
        }
        //Metodo remove

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);//remove o objeto desejado
            _context.SaveChanges();//confirma a remoção do objeto do banco de dados....

        } 
        //Metodo Update

        public void Update(Seller obj)//um serviço
        {
            if(!_context.Seller.Any(x => x.Id == obj.Id))//Verifica se no banco de dados existe um vendedor cujo o Id seja igual ao Id do objeto
            {
                throw new NotFoundException("Id not Found");
            }
            try
            {           
            _context.Update(obj);
            _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)//excessao do tipo acesso a dados
            {
                throw new DbConcurrencyException(e.Message);
                //usando a minha excessão em nivel de serviço para segregar as camadas, assim a camada de serviço não vai propagar uma excessao do nivel de acesso a dados

            }
        }

    }
}
