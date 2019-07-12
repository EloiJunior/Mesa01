using System;


namespace Mesa01.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        //construtor basico recebando a mensagem 
        public NotFoundException(string message) : base(message) //e esse construtor vai simplesmente repassar essa chamada para a classe base
        {
        }
    }
}
