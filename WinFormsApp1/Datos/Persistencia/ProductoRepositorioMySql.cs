using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Datos.Persistencia
{
    public class ProductoRepositorioMySql : IProductoRepositorio
    {
        public List<Producto> ObtenerTodos()
        {
            using var db = new PosDbContext();
            return db.Productos
                .AsNoTracking()
                .OrderBy(p => p.Nombre)
                .ToList();
        }

        public List<Producto> Buscar(string texto)
        {
            using var db = new PosDbContext();
            if (string.IsNullOrWhiteSpace(texto))
            {
                return ObtenerTodos();
            }

            var termino = $"%{texto}%";
            return db.Productos
                .AsNoTracking()
                .Where(p => EF.Functions.Like(p.Codigo, termino) ||
                            EF.Functions.Like(p.Nombre, termino))
                .OrderBy(p => p.Nombre)
                .ToList();
        }

        public void Agregar(Producto p)
        {
            using var db = new PosDbContext();
            db.Productos.Add(p);
            db.SaveChanges();
        }

        public void Actualizar(Producto p)
        {
            using var db = new PosDbContext();
            var existente = db.Productos.FirstOrDefault(x => x.Id == p.Id);
            if (existente == null) return;

            existente.Codigo = p.Codigo;
            existente.Nombre = p.Nombre;
            existente.Descripcion = p.Descripcion;
            existente.Precio = p.Precio;
            existente.Stock = p.Stock;

            db.SaveChanges();
        }

        public void Eliminar(int id)
        {
            using var db = new PosDbContext();
            var existente = db.Productos.FirstOrDefault(p => p.Id == id);
            if (existente == null) return;

            db.Productos.Remove(existente);
            db.SaveChanges();
        }

        public void DescontarStock(int productoId, int cantidad)
        {
            using var db = new PosDbContext();
            var producto = db.Productos.FirstOrDefault(p => p.Id == productoId);
            if (producto == null) return;

            producto.Stock -= cantidad;
            db.SaveChanges();
        }
    }
}
