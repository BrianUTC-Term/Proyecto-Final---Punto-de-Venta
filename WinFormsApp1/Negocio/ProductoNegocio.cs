using System.Collections.Generic;
using WinFormsApp1.Datos;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Negocio
{
    public static class ProductoNegocio
    {
        public static List<Producto> ObtenerTodos() => RepositorioFactory.Productos.ObtenerTodos();

        public static List<Producto> Buscar(string texto) => RepositorioFactory.Productos.Buscar(texto);

        public static void Agregar(Producto p)
        {
            Validar(p);
            RepositorioFactory.Productos.Agregar(p);
        }

        public static void Actualizar(Producto p)
        {
            Validar(p);
            RepositorioFactory.Productos.Actualizar(p);
        }

        public static void Eliminar(int id) => RepositorioFactory.Productos.Eliminar(id);

        private static void Validar(Producto p)
        {
            if (string.IsNullOrWhiteSpace(p.Codigo) || string.IsNullOrWhiteSpace(p.Nombre))
                throw new NegocioException("El codigo y el nombre del producto son obligatorios.");
            if (p.Precio < 0)
                throw new NegocioException("El precio no puede ser negativo.");
            if (p.Stock < 0)
                throw new NegocioException("El stock no puede ser negativo.");
        }
    }
}
