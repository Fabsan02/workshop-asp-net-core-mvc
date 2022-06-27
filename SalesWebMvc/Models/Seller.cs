using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]//torna o e-mail um link
        public string Email { get; set; }

        [Display(Name = "Birth Date")]//muda como o label vai aparecer no display
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]//zero indica o valor do atributo
        public double BaseSalary { get; set; }

        public Department Department  { get; set; }//Um seller possuir um departamento
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();//inicia a associação para muitas vendas contida na Lista de vendas feitas por vendedor 


        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        //Medodos Customizados 
        //Metodos Add adiciona um venda, remove remove um venda
        public void AddSales(SalesRecord sr)//metodo que adiciona um vendedor
        {
            Sales.Add(sr);//chama a operação Add recebendo sr;
        }

        public void RemoveSales(SalesRecord sr)//remove uma venda do vendedor
        {
            Sales.Remove(sr);
        }

        //Retorna o total de vendas num periodo de datas >>>>> Sr = SalesRecord
         public double TotalSales(DateTime initial, DateTime final)
        {
                        //filtra usando where                               //soma das vendas
            return Sales.Where(sr => sr.Date >= initial && sr.Date <=final).Sum(sr => sr.Amount);//
        }
            



    }
}
