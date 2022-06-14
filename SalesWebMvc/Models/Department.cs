using System;
using System.Linq;
using System.Collections.Generic;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();//Associação de departamento podendo ter uma lista com varios vendedores...


        public Department()
        {
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        //Adicionar um vendedor pegando alista de vendedores Sellers e add um vendedor
       public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }
        //clacular o valor total por departamentos
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));//pega cada vendedor da lista seller somando as vendas do vendedor dentro de um periodo somando o total para todos vemdedores do departamento .
        }


    }
}


//Para que o modelo seja implementado e reconhecido pelo entity framework temos que adicionar as classes DbSets em DbContext