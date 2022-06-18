using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;

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
        
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }



    }
}