using System;
using System.Linq;
using WinFormsApp1.Modelos;
using WinFormsApp1.Negocio;

namespace WinFormsApp1.Presentacion
{
    public static class MenuInventario
    {
        public static void Ejecutar()
        {
            string opcion;
            do
            {
                ConsolaHelper.Limpiar();
                Console.WriteLine("=== INVENTARIO Y CLIENTES ===");
                Console.WriteLine("1. Productos");
                Console.WriteLine("2. Clientes");
                Console.WriteLine("0. Volver al menu principal");
                opcion = ConsolaHelper.LeerOpcion();

                switch (opcion)
                {
                    case "1":
                        MenuProductos();
                        break;
                    case "2":
                        MenuClientes();
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

        // ---- Submenu Productos ----
        private static void MenuProductos()
        {
            string opcion;
            do
            {
                ConsolaHelper.Limpiar();
                Console.WriteLine("=== PRODUCTOS ===");
                Console.WriteLine("1. Listar productos");
                Console.WriteLine("2. Agregar producto");
                Console.WriteLine("3. Modificar producto");
                Console.WriteLine("4. Eliminar producto");
                Console.WriteLine("0. Volver");
                opcion = ConsolaHelper.LeerOpcion();

                switch (opcion)
                {
                    case "1":
                        ListarProductos();
                        ConsolaHelper.Pausar();
                        break;
                    case "2":
                        AgregarProducto();
                        ConsolaHelper.Pausar();
                        break;
                    case "3":
                        ModificarProducto();
                        ConsolaHelper.Pausar();
                        break;
                    case "4":
                        EliminarProducto();
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

        private static void ListarProductos()
        {
            Console.WriteLine();
            var productos = ProductoNegocio.ObtenerTodos();
            if (productos.Count == 0)
            {
                Console.WriteLine("No hay productos registrados.");
                return;
            }
            foreach (var p in productos)
                Console.WriteLine($"[{p.Id}] {p.Codigo} - {p.Nombre} - ${p.Precio:0.00} - Stock: {p.Stock}");
        }

        private static void AgregarProducto()
        {
            Console.WriteLine();
            Console.Write("Codigo: ");
            string codigo = (Console.ReadLine() ?? "").Trim();
            Console.Write("Nombre: ");
            string nombre = (Console.ReadLine() ?? "").Trim();
            Console.Write("Descripcion: ");
            string descripcion = (Console.ReadLine() ?? "").Trim();
            Console.Write("Precio: ");
            decimal.TryParse(Console.ReadLine(), out decimal precio);
            Console.Write("Stock: ");
            int.TryParse(Console.ReadLine(), out int stock);

            try
            {
                ProductoNegocio.Agregar(new Producto
                {
                    Codigo = codigo,
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Precio = precio,
                    Stock = stock
                });
                Console.WriteLine("Producto agregado.");
            }
            catch (NegocioException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static void ModificarProducto()
        {
            ListarProductos();
            Console.Write("Id del producto a modificar (0 para cancelar): ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id == 0) return;

            var producto = ProductoNegocio.ObtenerTodos().FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                Console.WriteLine("Producto no encontrado.");
                return;
            }

            Console.WriteLine("Deja en blanco para mantener el valor actual.");
            Console.Write($"Codigo [{producto.Codigo}]: ");
            string codigo = Console.ReadLine() ?? "";
            Console.Write($"Nombre [{producto.Nombre}]: ");
            string nombre = Console.ReadLine() ?? "";
            Console.Write($"Descripcion [{producto.Descripcion}]: ");
            string descripcion = Console.ReadLine() ?? "";
            Console.Write($"Precio [{producto.Precio}]: ");
            string precioTexto = Console.ReadLine() ?? "";
            Console.Write($"Stock [{producto.Stock}]: ");
            string stockTexto = Console.ReadLine() ?? "";

            producto.Codigo = string.IsNullOrWhiteSpace(codigo) ? producto.Codigo : codigo;
            producto.Nombre = string.IsNullOrWhiteSpace(nombre) ? producto.Nombre : nombre;
            producto.Descripcion = string.IsNullOrWhiteSpace(descripcion) ? producto.Descripcion : descripcion;
            if (decimal.TryParse(precioTexto, out decimal precio)) producto.Precio = precio;
            if (int.TryParse(stockTexto, out int stock)) producto.Stock = stock;

            try
            {
                ProductoNegocio.Actualizar(producto);
                Console.WriteLine("Producto actualizado.");
            }
            catch (NegocioException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static void EliminarProducto()
        {
            ListarProductos();
            Console.Write("Id del producto a eliminar (0 para cancelar): ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id == 0) return;

            Console.Write("Confirmar eliminacion? (S/N): ");
            string confirmacion = (Console.ReadLine() ?? "").Trim();
            if (!confirmacion.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Cancelado.");
                return;
            }

            ProductoNegocio.Eliminar(id);
            Console.WriteLine("Producto eliminado.");
        }

        // ---- Submenu Clientes ----
        private static void MenuClientes()
        {
            string opcion;
            do
            {
                ConsolaHelper.Limpiar();
                Console.WriteLine("=== CLIENTES ===");
                Console.WriteLine("1. Listar clientes");
                Console.WriteLine("2. Agregar cliente");
                Console.WriteLine("3. Modificar cliente");
                Console.WriteLine("4. Eliminar cliente");
                Console.WriteLine("0. Volver");
                opcion = ConsolaHelper.LeerOpcion();

                switch (opcion)
                {
                    case "1":
                        ListarClientes();
                        ConsolaHelper.Pausar();
                        break;
                    case "2":
                        AgregarCliente();
                        ConsolaHelper.Pausar();
                        break;
                    case "3":
                        ModificarCliente();
                        ConsolaHelper.Pausar();
                        break;
                    case "4":
                        EliminarCliente();
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

        private static void ListarClientes()
        {
            Console.WriteLine();
            var clientes = ClienteNegocio.ObtenerTodos();
            if (clientes.Count == 0)
            {
                Console.WriteLine("No hay clientes registrados.");
                return;
            }
            foreach (var c in clientes)
                Console.WriteLine($"[{c.Id}] {c.Nombre} - Tel: {c.Telefono} - Correo: {c.Correo}");
        }

        private static void AgregarCliente()
        {
            Console.WriteLine();
            Console.Write("Nombre: ");
            string nombre = (Console.ReadLine() ?? "").Trim();
            Console.Write("Telefono: ");
            string telefono = (Console.ReadLine() ?? "").Trim();
            Console.Write("Correo: ");
            string correo = (Console.ReadLine() ?? "").Trim();

            try
            {
                ClienteNegocio.Agregar(new Cliente { Nombre = nombre, Telefono = telefono, Correo = correo });
                Console.WriteLine("Cliente agregado.");
            }
            catch (NegocioException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static void ModificarCliente()
        {
            ListarClientes();
            Console.Write("Id del cliente a modificar (0 para cancelar): ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id == 0) return;

            var cliente = ClienteNegocio.ObtenerTodos().FirstOrDefault(c => c.Id == id);
            if (cliente == null)
            {
                Console.WriteLine("Cliente no encontrado.");
                return;
            }

            Console.WriteLine("Deja en blanco para mantener el valor actual.");
            Console.Write($"Nombre [{cliente.Nombre}]: ");
            string nombre = Console.ReadLine() ?? "";
            Console.Write($"Telefono [{cliente.Telefono}]: ");
            string telefono = Console.ReadLine() ?? "";
            Console.Write($"Correo [{cliente.Correo}]: ");
            string correo = Console.ReadLine() ?? "";

            cliente.Nombre = string.IsNullOrWhiteSpace(nombre) ? cliente.Nombre : nombre;
            cliente.Telefono = string.IsNullOrWhiteSpace(telefono) ? cliente.Telefono : telefono;
            cliente.Correo = string.IsNullOrWhiteSpace(correo) ? cliente.Correo : correo;

            try
            {
                ClienteNegocio.Actualizar(cliente);
                Console.WriteLine("Cliente actualizado.");
            }
            catch (NegocioException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static void EliminarCliente()
        {
            ListarClientes();
            Console.Write("Id del cliente a eliminar (0 para cancelar): ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id == 0) return;

            Console.Write("Confirmar eliminacion? (S/N): ");
            string confirmacion = (Console.ReadLine() ?? "").Trim();
            if (!confirmacion.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Cancelado.");
                return;
            }

            ClienteNegocio.Eliminar(id);
            Console.WriteLine("Cliente eliminado.");
        }
    }
}
