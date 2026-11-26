using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsApp1.Datos;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Negocio
{
    public static class VentaNegocio
    {
        // Valida la venta, recalcula el total y descuenta stock de forma atomica en Datos.
        public static void Registrar(VentaCabecera venta)
        {
            //validaciones de negocio
            if (venta.ClienteId <= 0)
                throw new NegocioException("Debes seleccionar un cliente.");
            if (venta.Detalles.Count == 0)
                throw new NegocioException("La venta no tiene productos.");

            var productos = RepositorioFactory.Productos.ObtenerTodos();
            decimal total = 0;

            foreach (var detalle in venta.Detalles)
            {
                if (detalle.Cantidad <= 0)
                    throw new NegocioException("La cantidad debe ser mayor a cero.");

                var producto = productos.FirstOrDefault(p => p.Id == detalle.ProductoId);
                if (producto == null)
                    throw new NegocioException("Uno de los productos de la venta ya no existe.");
                if (producto.Stock < detalle.Cantidad)
                    throw new NegocioException($"Stock insuficiente de '{producto.Nombre}'. Disponible: {producto.Stock}.");

                total += detalle.Cantidad * detalle.PrecioUnitario;
            }

            venta.Fecha = DateTime.Now;
            venta.Total = total;
            RepositorioFactory.Ventas.Registrar(venta);
        }

        public static List<VentaDetalle> ObtenerDetallesVendidos() => RepositorioFactory.Ventas.ObtenerDetallesVendidos();

        public static List<VentaCabecera> ObtenerVentasConCliente() => RepositorioFactory.Ventas.ObtenerVentasConCliente();
    }
}
