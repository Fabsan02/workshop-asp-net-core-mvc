using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

//Serviço que cria uma lista com os departamentos para que sejam acessados
namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;//criar uma dependecia para acessar o DbContext


        //criar um construtor para que a injeção de dependencia possa ocorrer

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;

        }

        //Metodo que retorna os departamentos ordenados por nome.

        /*public List<Department> FindAll()//Sincrona
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }*/

        //Assincrona
        public async Task <List<Department>> FindAllAsync(){      
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();//a chamada to list executa a chamada e transforma o resultado para list(ToList() e uma operação Sincrona) e mudamos para ToListAsync, assim nao bloqueia a aplicação.
        }



    }
}