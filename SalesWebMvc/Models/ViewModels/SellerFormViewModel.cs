using System.Collections.Generic;

namespace SalesWebMvc.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; } //Necessita de um vendedor
        public ICollection<Department>Departments{ get; set; }
    }
}
