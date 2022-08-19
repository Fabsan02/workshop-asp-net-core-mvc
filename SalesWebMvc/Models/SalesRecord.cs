using SalesWebMvc.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyy}")]//formatando a data
        public DateTime Date { get; set; }
        [DisplayFormat(DataFormatString ="{0:F2}")]//formatando o valor com duas casas decimais
        public double Amount { get; set; }
        public SaleStatus status { get; set; }
        public Seller Seller { get; set; }//Cada venda pertence a um vendedor.
        public ICollection<Product> Product = new List<Product> ( );

        public SalesRecord()//construtor sem argumentos
        {

        }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            this.status = status;
            Seller = seller;
        }
    }
}
