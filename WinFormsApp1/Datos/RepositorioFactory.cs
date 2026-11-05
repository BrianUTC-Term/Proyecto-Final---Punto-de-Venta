namespace WinFormsApp1.Datos
{
    // Punto de enlace entre negocio y datos.
    // La implementación activa usa Entity Framework Core con MySQL.

    public static class RepositorioFactory
    {
        private static readonly IProductoRepositorio productos = new Persistencia.ProductoRepositorioMySql();
        private static readonly IClienteRepositorio clientes = new Persistencia.ClienteRepositorioMySql();
        private static readonly IVentaRepositorio ventas = new Persistencia.VentaRepositorioMySql();

        public static IProductoRepositorio Productos => productos;
        public static IClienteRepositorio Clientes => clientes;
        public static IVentaRepositorio Ventas => ventas;
    }
}
