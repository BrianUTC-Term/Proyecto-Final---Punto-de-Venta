using System;

namespace WinFormsApp1.Presentacion
{
    public static class MenuPrincipal
    {
        public static void Ejecutar()
        {
            string opcion;
            do
            {
                ConsolaHelper.Limpiar();
                Console.WriteLine("=====================================");
                Console.WriteLine("   Sistema de Ventas - TechStore");
                Console.WriteLine("=====================================");
                Console.WriteLine("1. Facturacion (Punto de Venta)");
                Console.WriteLine("2. Inventario y Clientes");
                Console.WriteLine("3. Estadisticas");
                Console.WriteLine("0. Salir");
                opcion = ConsolaHelper.LeerOpcion();

                switch (opcion)
                {
                    case "1":
                        MenuFacturacion.Ejecutar();
                        break;
                    case "2":
                        MenuInventario.Ejecutar();
                        break;
                    case "3":
                        MenuEstadisticas.Ejecutar();
                        break;
                    case "0":
                        Console.WriteLine("Hasta luego.");
                        break;
                    default:
                        Console.WriteLine("Opcion invalida.");
                        ConsolaHelper.Pausar();
                        break;
                }
            }
            while (opcion != "0");
        }
    }
}
