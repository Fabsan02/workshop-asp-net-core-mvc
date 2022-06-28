using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        //Excessão personalizada de serviço para erros de integridade referencial
        public IntegrityException(string message) : base(message)
        {
        }
        
    }
}
