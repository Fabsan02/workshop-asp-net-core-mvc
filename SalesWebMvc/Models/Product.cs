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

        public int CategoryId { get; set; }
        public ICollection<SalesRecord> Sales = new List<SalesRecord> ( );
   
        public Product()
        {
        }

        public Product( int id, int code, string name, long priceUnit, DateTime date, Category category )
        {
            this.id = id;
            Code = code;
            Name = name;
            PriceUnit = priceUnit;
            Date = date;
            Category = category;
        }

        //Medodos Customizados 
        //Metodos Add adiciona um venda, remove remove um venda
        public void AddSales( SalesRecord sr )//metodo que adiciona um vendedor
        {
            Sales.Add ( sr );//chama a operação Add recebendo sr;
        }

        public void RemoveSales( SalesRecord sr )//remove uma venda do vendedor
        {
            Sales.Remove ( sr );
        }

        //Retorna o total de vendas num periodo de datas >>>>> Sr = SalesRecord
        public double TotalSales( DateTime initial, DateTime final )
        {
            //filtra usando where                               //soma das vendas
            return Sales.Where ( sr => sr.Date >= initial && sr.Date <= final ).Sum ( sr => sr.Amount);//
        }


    }
  }

