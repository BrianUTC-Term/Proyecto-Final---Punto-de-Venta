using System.Collections.Generic;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Datos
{
    // Contrato de acceso a datos de productos.
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
