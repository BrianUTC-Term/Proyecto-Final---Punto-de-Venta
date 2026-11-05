using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Datos.Persistencia
{
    public class VentaRepositorioMySql : IVentaRepositorio
    {
        public void Registrar(VentaCabecera venta)
        {
            using var db = new PosDbContext();
            using var tx = db.Database.BeginTransaction();

            db.VentasCabecera.Add(venta);
            db.SaveChanges();

            foreach (var detalle in venta.Detalles)
            {
                detalle.VentaId = venta.Id;
                db.VentasDetalle.Add(detalle);

                var producto = db.Productos.FirstOrDefault(p => p.Id == detalle.ProductoId);
                if (producto != null)
                {
                    producto.Stock -= detalle.Cantidad;
                }
            }

            db.SaveChanges();
            tx.Commit();
        }

        public List<VentaDetalle> ObtenerDetallesVendidos()
        {
            using var db = new PosDbContext();

            return (from d in db.VentasDetalle.AsNoTracking()
                    join p in db.Productos.AsNoTracking() on d.ProductoId equals p.Id
                    select new VentaDetalle
                    {
                        Id = d.Id,
                        VentaId = d.VentaId,
                        ProductoId = d.ProductoId,
                        ProductoNombre = p.Nombre,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario
                    })
                .ToList();
        }

        public List<VentaCabecera> ObtenerVentasConCliente()
        {
            using var db = new PosDbContext();

            return (from v in db.VentasCabecera.AsNoTracking()
                    join c in db.Clientes.AsNoTracking() on v.ClienteId equals c.Id
                    select new VentaCabecera
                    {
                        Id = v.Id,
                        Fecha = v.Fecha,
                        ClienteId = v.ClienteId,
                        ClienteNombre = c.Nombre,
                        Total = v.Total
                    })
                .ToList();
        }
    }
}
