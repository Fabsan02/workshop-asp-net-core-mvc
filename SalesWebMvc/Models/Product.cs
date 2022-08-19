using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class Product
    {
        public int id { get; set; }
        public int Code { get; set; }
        public String Name { get; set; }
        public long PriceUnit { get; set; }
        public DateTime Date { get; set; }
        public Category Category { get; set; }

        public Product() { }

        public Product( int id, int code, string name, long priceUnit, DateTime date, Category category )
        {
            this.id = id;
            Code = code;
            Name = name;
            PriceUnit = priceUnit;
            Date = date;
            Category = category;
        }

    }
}
