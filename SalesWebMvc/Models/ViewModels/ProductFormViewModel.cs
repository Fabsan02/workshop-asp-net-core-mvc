using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models.ViewModels
{
    public class ProductFormViewModel
    {

        public Product Product{ get; set; } //Necessita de um vendedor
        public ICollection<Category> Categories { get; set; }
        public List<Category> Category { get; internal set; }
    }
}
