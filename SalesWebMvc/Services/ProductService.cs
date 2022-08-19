using SalesWebMvc.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;
using System.Threading.Tasks;

namespace SalesWebMvc.Services //registrar este serviço no
{
    public class ProductService
    {
        private readonly SalesWebMvcContext _context;//criar uma dependecia para acessar o DbContext


        //criar um construtor para que a injeção de dependencia possa ocorrer

        public ProductService( SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task< List<Product>>FindAllAsync() //implementar uma operação no entity framework
        {
            return await _context.Products.ToListAsync();//acessa a tabela de vendedores e convert em uma lista(acessa o banco de dados)
        }

       // public void Insert(Seller obj)//insere um objeto no banco de dados..forma Sincrona
        public async Task InsertAsync(Product obj)//insere um objeto no banco de dados...Forma Assincrona
        {
            _context.Add(obj);//e uma operação feita somente em memoria
            await _context.SaveChangesAsync();//acessa e atualiza o banco de dados ( SaveChanges() forma Sincrona)
        }

        public async Task< Product> FindByIdAsync(int id)
        {
            return await _context.Products.Include(obj => obj.Code).FirstOrDefaultAsync(obj => obj.id == id);//acessa o banco de dados com FirstOrDefault(obj => obj.Id == id)
        }

        //Metodo remove

        public async Task RemoveAsync(int id)
        {
            try
            { 

                var obj = await _context.Products.FindAsync(id);
                _context.Products.Remove(obj);//remove o objeto desejado
                await _context.SaveChangesAsync();//confirma a remoção do objeto do banco de dados....
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

       
        //Metodo Update

        public async Task Update(Product obj)//um serviço
        {
            bool hasAny = await _context.Products.AnyAsync(x => x.id == obj.id);
            if (!hasAny)//Verifica se no banco de dados existe um vendedor cujo o Id seja igual ao Id do objeto
            {
                throw new NotFoundException("Id not Found");
            }
            try
            {           
                 _context.Update(obj);
           await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)//excessao do tipo acesso a dados
            {
                throw new DbConcurrencyException(e.Message);
                //usando a minha excessão em nivel de serviço para segregar as camadas, assim a camada de serviço não vai propagar uma excessao do nivel de acesso a dados

            }
        }
    }
}
