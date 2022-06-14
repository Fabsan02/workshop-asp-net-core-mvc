using System.Threading.Tasks;

namespace SalesWebMvc.Models.Enums
{
    public enum SaleStatus :int
    {
     Pending = 0, //
     Billed = 1,//Faturado
     Cancelled = 2 //cancelado
    }
}
