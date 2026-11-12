using MySql.Data.MySqlClient;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Datos.Persistencia
{
    public class VentaRepositorioMySql : IVentaRepositorio
    {
        public void Registrar(VentaCabecera venta)
        {
            const string sqlCabecera = @"
                INSERT INTO venta_cabecera (fecha, cliente_id, total)
                VALUES (@fecha, @clienteId, @total);
                SELECT LAST_INSERT_ID();";

            const string sqlDetalle = @"
                INSERT INTO venta_detalle (venta_id, producto_id, cantidad, precio_unitario)
                VALUES (@ventaId, @productoId, @cantidad, @precioUnitario);
                SELECT LAST_INSERT_ID();";

            const string sqlDescontarStock = @"
                UPDATE productos
                SET stock = stock - @cantidad
                WHERE id = @productoId;";

            using var connection = new MySqlConnection(ConexionDb.CadenaConexion);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                using (var comandoCabecera = new MySqlCommand(sqlCabecera, connection, transaction))
                {
                    comandoCabecera.Parameters.AddWithValue("@fecha", venta.Fecha);
                    comandoCabecera.Parameters.AddWithValue("@clienteId", venta.ClienteId);
                    comandoCabecera.Parameters.AddWithValue("@total", venta.Total);
                    venta.Id = Convert.ToInt32(comandoCabecera.ExecuteScalar());
                }

                foreach (var detalle in venta.Detalles)
                {
                    detalle.VentaId = venta.Id;

                    using (var comandoDetalle = new MySqlCommand(sqlDetalle, connection, transaction))
                    {
                        comandoDetalle.Parameters.AddWithValue("@ventaId", detalle.VentaId);
                        comandoDetalle.Parameters.AddWithValue("@productoId", detalle.ProductoId);
                        comandoDetalle.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                        comandoDetalle.Parameters.AddWithValue("@precioUnitario", detalle.PrecioUnitario);
                        detalle.Id = Convert.ToInt32(comandoDetalle.ExecuteScalar());
                    }

                    using var comandoStock = new MySqlCommand(sqlDescontarStock, connection, transaction);
                    comandoStock.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    comandoStock.Parameters.AddWithValue("@productoId", detalle.ProductoId);
                    comandoStock.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public List<VentaDetalle> ObtenerDetallesVendidos()
        {
            const string sql = @"
                SELECT d.id, d.venta_id, d.producto_id, p.nombre AS producto_nombre, d.cantidad, d.precio_unitario
                FROM venta_detalle d
                INNER JOIN productos p ON p.id = d.producto_id;";

            var detalles = new List<VentaDetalle>();
            using var connection = new MySqlConnection(ConexionDb.CadenaConexion);
            connection.Open();

            using var comando = new MySqlCommand(sql, connection);
            using var reader = comando.ExecuteReader();
            while (reader.Read())
            {
                detalles.Add(new VentaDetalle
                {
                    Id = reader.GetInt32("id"),
                    VentaId = reader.GetInt32("venta_id"),
                    ProductoId = reader.GetInt32("producto_id"),
                    ProductoNombre = reader.GetString("producto_nombre"),
                    Cantidad = reader.GetInt32("cantidad"),
                    PrecioUnitario = reader.GetDecimal("precio_unitario")
                });
            }

            return detalles;
        }

        public List<VentaCabecera> ObtenerVentasConCliente()
        {
            const string sql = @"
                SELECT v.id, v.fecha, v.cliente_id, c.nombre AS cliente_nombre, v.total
                FROM venta_cabecera v
                INNER JOIN clientes c ON c.id = v.cliente_id;";

            var ventas = new List<VentaCabecera>();
            using var connection = new MySqlConnection(ConexionDb.CadenaConexion);
            connection.Open();

            using var comando = new MySqlCommand(sql, connection);
            using var reader = comando.ExecuteReader();
            while (reader.Read())
            {
                ventas.Add(new VentaCabecera
                {
                    Id = reader.GetInt32("id"),
                    Fecha = reader.GetDateTime("fecha"),
                    ClienteId = reader.GetInt32("cliente_id"),
                    ClienteNombre = reader.GetString("cliente_nombre"),
                    Total = reader.GetDecimal("total")
                });
            }

            return ventas;
        }
    }
}
