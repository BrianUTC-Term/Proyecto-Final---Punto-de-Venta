using System;

namespace WinFormsApp1.Negocio
{
    // Excepcion para errores de reglas de negocio (distinta de errores de conexion/BD).
    public class NegocioException : Exception
    {
        public NegocioException(string mensaje) : base(mensaje)
        {
        }
    }
}
