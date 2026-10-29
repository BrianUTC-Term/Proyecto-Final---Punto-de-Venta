using System.Collections.Generic;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Datos
{
    // Contrato de acceso a datos de productos.
    // Para conectar la base de datos real: crea una clase que implemente esta interfaz
    // (ej. ProductoRepositorioMySql) y registrala en RepositorioFactory.
    public interface IProductoRepositorio
    {
        List<Producto> ObtenerTodos();
        List<Producto> Buscar(string texto);
        void Agregar(Producto p);
        void Actualizar(Producto p);
        void Eliminar(int id);
        void DescontarStock(int productoId, int cantidad);
    }
}
