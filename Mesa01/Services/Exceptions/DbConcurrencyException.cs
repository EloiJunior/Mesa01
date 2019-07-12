using System;


namespace Mesa01.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException  // vai herdar de outra classe
    {
        //construtor basico
        public DbConcurrencyException(string message) : base(message) // vai simplesmente repassar o argumento para a classe base
        {
        }
    }
}
