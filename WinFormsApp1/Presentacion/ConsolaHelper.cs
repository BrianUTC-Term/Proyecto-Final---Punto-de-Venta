using System;
using System.IO;

namespace WinFormsApp1.Presentacion
{
    public static class ConsolaHelper
    {
        public static void Limpiar()
        {
            try
            {
                Console.Clear();
            }
            catch (IOException)
            {
                // No hay una consola real adjunta (salida redirigida); se ignora.
            }
        }

        public static void Pausar()
        {
            Console.WriteLine();
            Console.WriteLine("Presiona ENTER para continuar...");
            Console.ReadLine();
        }

        public static string LeerOpcion()
        {
            Console.Write("Opcion: ");
            return (Console.ReadLine() ?? "").Trim();
        }
    }
}
