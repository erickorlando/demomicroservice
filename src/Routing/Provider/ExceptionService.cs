using System;

namespace Provider
{
    public class ExceptionService : Exception
    {
        public ExceptionService() { }

        public ExceptionService(string message) : base(message) { }

        public ExceptionService(Exception inner) : base(inner.Message, inner)
        {
            inner.HelpLink = "1";
            inner.Source = "Error en el sistema";
        }

        public ExceptionService(int status, Exception inner) : base(inner.Message, inner)
        {
            inner.HelpLink = "1";
            inner.Source = "Error en el sistema";
        }

        public ExceptionService(string message, Exception inner) : base(message, inner)
        {
            inner.HelpLink = "1";
            inner.Source = "Error en el sistema";
        }
    }
}
