using System;
using System.Linq;
using WinFormsApp1.Negocio;

namespace WinFormsApp1.Presentacion
{
    public static class MenuEstadisticas
    {
        private const int UmbralStockBajo = 10;

        public static void Ejecutar()
        {
            string opcion;
            do
            {
                ConsolaHelper.Limpiar();
                Console.WriteLine("=== ESTADISTICAS ===");
                Console.WriteLine("1. Productos mas vendidos");
                Console.WriteLine("2. Clientes mas frecuentes");
                Console.WriteLine("3. Productos con stock bajo");
                Console.WriteLine("0. Volver al menu principal");
                opcion = ConsolaHelper.LeerOpcion();

                switch (opcion)
                {
                    case "1":
                        MostrarProductosMasVendidos();
                        ConsolaHelper.Pausar();
                        break;
                    case "2":
                        MostrarClientesFrecuentes();
                        ConsolaHelper.Pausar();
                        break;
                    case "3":
                        MostrarStockBajo();
                        ConsolaHelper.Pausar();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Opcion invalida.");
                        ConsolaHelper.Pausar();
                        break;
                }
            }
            while (opcion != "0");
        }

        // Producto mas vendido: se agrupan con LINQ todos los detalles de venta.
        private static void MostrarProductosMasVendidos()
        {
            Console.WriteLine();
            var top = VentaNegocio.ObtenerDetallesVendidos()
                .GroupBy(d => d.ProductoNombre)
                .Select(g => new { Producto = g.Key, Total = g.Sum(x => x.Cantidad) })
                .OrderByDescending(x => x.Total)
                .Take(5)
                .ToList();

            if (top.Count == 0)
            {
                Console.WriteLine("Aun no hay ventas registradas.");
                return;
            }
            foreach (var item in top)
                Console.WriteLine($"{item.Producto}: {item.Total} unidades vendidas");
        }

        // Clientes mas frecuentes: se agrupan con LINQ las ventas por cliente.
        private static void MostrarClientesFrecuentes()
        {
            Console.WriteLine();
            var top = VentaNegocio.ObtenerVentasConCliente()
                .GroupBy(v => v.ClienteNombre)
                .Select(g => new { Cliente = g.Key, Compras = g.Count() })
                .OrderByDescending(x => x.Compras)
                .Take(5)
                .ToList();

            if (top.Count == 0)
            {
                Console.WriteLine("Aun no hay ventas registradas.");
                return;
            }
            foreach (var item in top)
                Console.WriteLine($"{item.Cliente}: {item.Compras} compra(s)");
        }

        // Productos con stock bajo: alerta para contactar al proveedor.
        private static void MostrarStockBajo()
        {
            Console.WriteLine();
            var bajoStock = ProductoNegocio.ObtenerTodos()
                .Where(p => p.Stock <= UmbralStockBajo)
                .OrderBy(p => p.Stock)
                .ToList();

            if (bajoStock.Count == 0)
            {
                Console.WriteLine("No hay productos con stock bajo.");
                return;
            }
            foreach (var p in bajoStock)
                Console.WriteLine($"{p.Nombre} - Stock: {p.Stock}");
        }
    }
}
