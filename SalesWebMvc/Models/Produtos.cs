using System. Collections. Generic;
using System. ComponentModel. DataAnnotations;
using System. ComponentModel. DataAnnotations. Schema;
namespace SalesWebMvc.Models
{
    public class Produtos
    {
        [Key]
        public int Id  { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public bool Ativo { get; set; }  

    }
}
