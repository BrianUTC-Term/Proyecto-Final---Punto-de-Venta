using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Datos
{
    // Simulacion en memoria (lista) mientras no se conecta la base de datos real.
    public class ProductoRepositorioMemoria : IProductoRepositorio
    {
        private readonly List<Producto> productos = new()
        {
            new Producto { Id = 1, Codigo = "T001", Nombre = "Laptop Core i5", Descripcion = "Laptop 15.6\" Core i5 8GB RAM", Precio = 650.00m, Stock = 15 },
            new Producto { Id = 2, Codigo = "T002", Nombre = "Mouse Inalambrico", Descripcion = "Mouse inalambrico optico", Precio = 12.50m, Stock = 40 },
            new Producto { Id = 3, Codigo = "T003", Nombre = "Teclado Mecanico", Descripcion = "Teclado mecanico RGB", Precio = 35.00m, Stock = 25 },
            new Producto { Id = 4, Codigo = "T004", Nombre = "Celular Galaxy A15", Descripcion = "Smartphone 128GB 4G", Precio = 180.00m, Stock = 8 },
            new Producto { Id = 5, Codigo = "T005", Nombre = "Monitor 24 pulgadas", Descripcion = "Monitor LED Full HD", Precio = 110.00m, Stock = 5 }
        };
        private int siguienteId = 6;

        public List<Producto> ObtenerTodos() =>
            productos.OrderBy(p => p.Nombre).ToList();

        public List<Producto> Buscar(string texto) =>
            productos
                .Where(p => p.Codigo.Contains(texto, StringComparison.OrdinalIgnoreCase) ||
                            p.Nombre.Contains(texto, StringComparison.OrdinalIgnoreCase))
                .OrderBy(p => p.Nombre)
                .ToList();

        public void Agregar(Producto p)
        {
            p.Id = siguienteId++;
            productos.Add(p);
        }

        public void Actualizar(Producto p)
        {
            var existente = productos.FirstOrDefault(x => x.Id == p.Id);
            if (existente == null) return;
            existente.Codigo = p.Codigo;
            existente.Nombre = p.Nombre;
            existente.Descripcion = p.Descripcion;
            existente.Precio = p.Precio;
            existente.Stock = p.Stock;
        }

        public void Eliminar(int id)
        {
            productos.RemoveAll(p => p.Id == id);
        }

        public void DescontarStock(int productoId, int cantidad)
        {
            var producto = productos.FirstOrDefault(p => p.Id == productoId);
            if (producto != null)
                producto.Stock -= cantidad;
        }
    }
}
