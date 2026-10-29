namespace WinFormsApp1.Datos
{
    //Este es el factory, el unico PUNTO DE ELNACE  entre la capa de NEGOCIO/ y la de DATOS/
    //Si cambio a mysql, solo debo implementar la clase nueva a la derecha del =
    //EJEMPLO: private static readonly IProductoRepositorio productos = new ProductoRepositorioMySQL(); y que esa nueva clase cumpla con la interfaz.
    //Ya con eso en la capa de negocio no debo tocar nada.
    //MUY IMPORTANTE: usar interfaces para asegurarme que las nuevas clases implementen todos los metodos/atributos que se estan usando en negocio/ para evitar errores.

    public static class RepositorioFactory
    {
        private static readonly IProductoRepositorio productos = new ProductoRepositorioMemoria(); 
        private static readonly IClienteRepositorio clientes = new ClienteRepositorioMemoria();
        private static readonly IVentaRepositorio ventas = new VentaRepositorioMemoria(productos, clientes);

        public static IProductoRepositorio Productos => productos;
        public static IClienteRepositorio Clientes => clientes;
        public static IVentaRepositorio Ventas => ventas;
    }
}
