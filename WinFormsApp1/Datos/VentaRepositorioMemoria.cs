using System.Collections.Generic;
using System.Linq;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Datos
{
    // Simulacion en memoria (lista) mientras no se conecta la base de datos real.
    public class VentaRepositorioMemoria : IVentaRepositorio
    {
        private readonly List<VentaCabecera> ventas = new();
        private int siguienteVentaId = 1;
        private int siguienteDetalleId = 1;

        private readonly IProductoRepositorio productoRepositorio;
        private readonly IClienteRepositorio clienteRepositorio;

        public VentaRepositorioMemoria(IProductoRepositorio productoRepositorio, IClienteRepositorio clienteRepositorio)
        {
            this.productoRepositorio = productoRepositorio;
            this.clienteRepositorio = clienteRepositorio;
        }

        // Registra la cabecera y el detalle de la venta, y descuenta el stock vendido.
        public void Registrar(VentaCabecera venta)
        {
            venta.Id = siguienteVentaId++;
            foreach (var detalle in venta.Detalles)
            {
                detalle.Id = siguienteDetalleId++;
                detalle.VentaId = venta.Id;
                productoRepositorio.DescontarStock(detalle.ProductoId, detalle.Cantidad);
            }
            ventas.Add(venta);
        }

        // Detalle de todas las ventas, usado para calcular el producto mas vendido con LINQ.
        public List<VentaDetalle> ObtenerDetallesVendidos()
        {
            var productos = productoRepositorio.ObtenerTodos(); //devuelve la lista completa de productos que esta en ProductoRepositorioMemoria
            return ventas                                       //con LINQ se recorre y se filtra los datos a la vez. ES EL EQUIVALENTE a un foreach + if de validaciones.
                .SelectMany(v => v.Detalles)
                .Select(d => new VentaDetalle
                {
                    Id = d.Id,
                    VentaId = d.VentaId,
                    ProductoId = d.ProductoId,
                    ProductoNombre = productos.FirstOrDefault(p => p.Id == d.ProductoId)?.Nombre ?? "",
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario
                })
                .ToList();
        }

        // Cabeceras de venta con el nombre del cliente, usado para calcular clientes frecuentes con LINQ.
        public List<VentaCabecera> ObtenerVentasConCliente()
        {
            var clientes = clienteRepositorio.ObtenerTodos();
            return ventas
                .Select(v => new VentaCabecera
                {
                    Id = v.Id,
                    Fecha = v.Fecha,
                    ClienteId = v.ClienteId,
                    ClienteNombre = clientes.FirstOrDefault(c => c.Id == v.ClienteId)?.Nombre ?? "",
                    Total = v.Total
                })
                .ToList();
        }
    }
}
