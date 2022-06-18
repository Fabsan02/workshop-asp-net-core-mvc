using System;
using System.Linq;
using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Data
{
    public class SeedingService//quando for criado um SeedingService automaticamente ele vai receber uma instancia do context tambem
    {
        private SalesWebMvcContext _context; //esta linha indica que a injeção de dependencia deve aconstece

        public SeedingService(SalesWebMvcContext context)
        {
            _context = context;
        }
        //operação responsavel por popular o banco de dados
        public void Seed()
        {
            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any())//verifica se ja existe algum dado no banco de dado, se existir algum resgistro se exisitr não faz nada.
            {
                return;//DB has been seeded
            }
            
            //Populando os departamentos

            Department d1 = new Department(1, "Bolsas");
            Department d2 = new Department(2, "Carteiras");
            Department d3 = new Department(3, "Relogios");
            Department d4 = new Department(4, "Cosmeticos");
           

            //Popular os vendedor
            Seller s1 = new Seller(1,"Karina Soares", "ksoares173@gmail.com", new DateTime(1982, 11, 25), 1000.00, d1);
            
            //Registros de Vendas

            SalesRecord r1 = new SalesRecord(1, new DateTime(2018, 09, 25), 1100.0, SaleStatus.Billed, s1);


            //inclusão dos objetos do banco de dados..

            _context.Department.AddRange(d1);
            _context.Seller.AddRange(s1);
            _context.SalesRecord.AddRange(r1);

            //feita a inclusão dos dados no banco de dados

            _context.SaveChanges();//salva e confirma as alterações do banco de dados....

        }
    }
}


