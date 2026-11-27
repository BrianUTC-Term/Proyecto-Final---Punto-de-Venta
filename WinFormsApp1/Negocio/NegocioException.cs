using System;

namespace WinFormsApp1.Negocio
{
    public class NegocioException : Exception
    {
        public NegocioException(string mensaje) : base(mensaje)
        {
        }
    }
}
