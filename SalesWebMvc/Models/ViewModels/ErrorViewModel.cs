using System;

namespace SalesWebMvc.Models.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; }//possibilita acrecentar uma mensagem customizada no objeto ErrorViewModel

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);//fun��o que verifica a existencia do Id n�o e nullo ou vazio
    }
}