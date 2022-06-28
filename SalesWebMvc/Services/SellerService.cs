using SalesWebMvc.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;
using System.Threading.Tasks;

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

        public async Task< List<Seller>>FindAllAsync() //implementar uma operação no entity framework
        {
            return await _context.Seller.ToListAsync();//acessa a tabela de vendedores e convert em uma lista(acessa o banco de dados)
        }

       // public void Insert(Seller obj)//insere um objeto no banco de dados..forma Sincrona
        public async Task InsertAsync(Seller obj)//insere um objeto no banco de dados...Forma Assincrona
        {
            _context.Add(obj);//e uma operação feita somente em memoria
            await _context.SaveChangesAsync();//acessa e atualiza o banco de dados ( SaveChanges() forma Sincrona)
        }

        public async Task< Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);//acessa o banco de dados com FirstOrDefault(obj => obj.Id == id)
        }

        //Metodo remove

        public async Task RemoveAsync(int id)
        {
            try
            { 

                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);//remove o objeto desejado
                await _context.SaveChangesAsync();//confirma a remoção do objeto do banco de dados....
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

       
        //Metodo Update

        public async Task Update(Seller obj)//um serviço
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
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
