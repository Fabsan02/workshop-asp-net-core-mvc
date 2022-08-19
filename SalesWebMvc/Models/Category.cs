using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class Category
    {
        public int id { get; set; }
        public string Name { get; set; }
        public ICollection<Product>Products = new List<Product>( );

        public Category()
        {

        }

        public Category( int id, string name )
        {
            this.id = id;
            Name = name;
        }
    }
}
