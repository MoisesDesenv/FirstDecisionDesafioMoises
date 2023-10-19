using System;

namespace FirstDecisionDesafioMoises.Infraestructure.Exceptions
{
    public class LimiteTextoExcedidoException : Exception
    {
        public LimiteTextoExcedidoException(string message) : base(message) { }
    }
}
