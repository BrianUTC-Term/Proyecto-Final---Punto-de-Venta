using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsApp1.Modelos;
using WinFormsApp1.Negocio;

namespace WinFormsApp1.Presentacion
{
    public static class MenuFacturacion
    {
        public static void Ejecutar()
        {
            ConsolaHelper.Limpiar();
            Console.WriteLine("=== FACTURACION ===");

            var cliente = SeleccionarCliente();
            if (cliente == null)
            {
                Console.WriteLine("Venta cancelada.");
                ConsolaHelper.Pausar();
                return;
            }

            var detalles = new List<VentaDetalle>();
            bool continuar = true;
            do
            {
                MostrarCarrito(detalles);
                Console.WriteLine();
                Console.WriteLine($"Cliente: {cliente.Nombre}");
                Console.WriteLine("1. Agregar producto");
                Console.WriteLine("2. Quitar producto");
                Console.WriteLine("3. Finalizar venta");
                Console.WriteLine("0. Cancelar venta");
                string opcion = ConsolaHelper.LeerOpcion();

                switch (opcion)
                {
                    case "1":
                        AgregarProducto(detalles);
                        break;
                    case "2":
                        QuitarProducto(detalles);
                        break;
                    case "3":
                        if (FinalizarVenta(cliente, detalles))
                            continuar = false;
                        break;
                    case "0":
                        Console.WriteLine("Venta cancelada.");
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opcion invalida.");
                        break;
                }
            }
            while (continuar);

            ConsolaHelper.Pausar();
        }

        private static Cliente? SeleccionarCliente()
        {
            do
            {
                Console.WriteLine();
                Console.WriteLine("--- Seleccionar cliente ---");
                var clientes = ClienteNegocio.ObtenerTodos();
                for (int i = 0; i < clientes.Count; i++)
                    Console.WriteLine($"{i + 1}. {clientes[i].Nombre} ({clientes[i].Telefono})");
                Console.WriteLine("N. Registrar cliente nuevo");
                Console.WriteLine("0. Cancelar");
                string opcion = ConsolaHelper.LeerOpcion();

                if (opcion.Equals("N", StringComparison.OrdinalIgnoreCase))
                    return CrearClienteRapido();

                if (opcion == "0")
                    return null;

                if (int.TryParse(opcion, out int indice) && indice >= 1 && indice <= clientes.Count)
                    return clientes[indice - 1];

                Console.WriteLine("Opcion invalida.");
            }
            while (true);
        }

        private static Cliente? CrearClienteRapido()
        {
            Console.WriteLine();
            Console.WriteLine("--- Nuevo cliente (escribe 0 en el nombre para cancelar) ---");
            Cliente? creado = null;
            bool listo = false;
            do
            {
                Console.Write("Nombre: ");
                string nombre = (Console.ReadLine() ?? "").Trim();
                if (nombre == "0") return null;

                Console.Write("Telefono: ");
                string telefono = (Console.ReadLine() ?? "").Trim();
                Console.Write("Correo: ");
                string correo = (Console.ReadLine() ?? "").Trim();

                try
                {
                    var cliente = new Cliente { Nombre = nombre, Telefono = telefono, Correo = correo };
                    cliente.Id = ClienteNegocio.Agregar(cliente);
                    Console.WriteLine("Cliente creado.");
                    creado = cliente;
                    listo = true;
                }
                catch (NegocioException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            while (!listo);

            return creado;
        }

        private static void AgregarProducto(List<VentaDetalle> detalles)
        {
            Console.WriteLine();
            Console.Write("Buscar producto (nombre o codigo, vacio = todos): ");
            string texto = (Console.ReadLine() ?? "").Trim();
            var productos = string.IsNullOrWhiteSpace(texto)
                ? ProductoNegocio.ObtenerTodos()
                : ProductoNegocio.Buscar(texto);

            if (productos.Count == 0)
            {
                Console.WriteLine("No se encontraron productos.");
                return;
            }

            for (int i = 0; i < productos.Count; i++)
                Console.WriteLine($"{i + 1}. {productos[i].Nombre} - ${productos[i].Precio:0.00} (Stock: {productos[i].Stock})");
            Console.Write("Selecciona un producto (0 para cancelar): ");

            if (!int.TryParse(Console.ReadLine(), out int indice) || indice == 0)
                return;
            if (indice < 1 || indice > productos.Count)
            {
                Console.WriteLine("Opcion invalida.");
                return;
            }

            var producto = productos[indice - 1];
            Console.Write("Cantidad: ");
            if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
            {
                Console.WriteLine("Cantidad invalida.");
                return;
            }

            var existente = detalles.FirstOrDefault(d => d.ProductoId == producto.Id);
            if (existente != null)
                existente.Cantidad += cantidad;
            else
                detalles.Add(new VentaDetalle
                {
                    ProductoId = producto.Id,
                    ProductoNombre = producto.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio
                });

            Console.WriteLine("Producto agregado al carrito.");
        }

        private static void QuitarProducto(List<VentaDetalle> detalles)
        {
            if (detalles.Count == 0)
            {
                Console.WriteLine("El carrito esta vacio.");
                return;
            }

            MostrarCarrito(detalles);
            Console.Write("Selecciona el numero a quitar (0 para cancelar): ");
            if (!int.TryParse(Console.ReadLine(), out int indice) || indice == 0)
                return;
            if (indice < 1 || indice > detalles.Count)
            {
                Console.WriteLine("Opcion invalida.");
                return;
            }

            detalles.RemoveAt(indice - 1);
            Console.WriteLine("Producto quitado.");
        }

        private static void MostrarCarrito(List<VentaDetalle> detalles)
        {
            Console.WriteLine();
            Console.WriteLine("--- Carrito ---");
            if (detalles.Count == 0)
            {
                Console.WriteLine("(vacio)");
                return;
            }

            decimal total = 0;
            for (int i = 0; i < detalles.Count; i++)
            {
                decimal subtotal = detalles[i].Cantidad * detalles[i].PrecioUnitario;
                total += subtotal;
                Console.WriteLine($"{i + 1}. {detalles[i].ProductoNombre} x{detalles[i].Cantidad} - ${subtotal:0.00}");
            }
            Console.WriteLine($"TOTAL: ${total:0.00}");
        }

        private static bool FinalizarVenta(Cliente cliente, List<VentaDetalle> detalles)
        {
            if (detalles.Count == 0)
            {
                Console.WriteLine("El carrito esta vacio.");
                return false;
            }

            var venta = new VentaCabecera { ClienteId = cliente.Id, Detalles = detalles };
            try
            {
                VentaNegocio.Registrar(venta);
                Console.WriteLine();
                Console.WriteLine($"Factura generada. Total: ${venta.Total:0.00}");
                return true;
            }
            catch (NegocioException ex)
            {
                Console.WriteLine("No se pudo generar la factura: " + ex.Message);
                return false;
            }
        }
    }
}
