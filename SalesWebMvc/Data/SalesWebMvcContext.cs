﻿
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Models
{
    public class SalesWebMvcContext : DbContext
    {
        public SalesWebMvcContext (DbContextOptions<SalesWebMvcContext> options) : base(options)
        {

        }

        //Add Dbset para as entidades Departamento, Vendedor,Vendas(tres entidades

        public DbSet<Department> Department { get; set; }
        public DbSet<Seller>Seller { get; set; }
        public DbSet<SalesRecord>SalesRecord { get; set; }

    }
}
