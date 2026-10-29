using System;
using WinFormsApp1.Presentacion;

namespace WinFormsApp1
{
    internal static class Program
    {
        static void Main()
        {
            try
            {
                MenuPrincipal.Ejecutar();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Ocurrio un error inesperado: " + ex.Message);
            }
        }
    }
}
