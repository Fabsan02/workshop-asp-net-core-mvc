using System;

namespace SalesWebMvc.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; }//possibilita acrecentar uma mensagem customizada no objeto ErrorViewModel

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);//função que verifica a existencia do Id não e nullo ou vazio
    }
}