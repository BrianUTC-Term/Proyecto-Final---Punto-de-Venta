using MySql.Data.MySqlClient;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Datos.Persistencia
{
    public class ProductoRepositorioMySql : IProductoRepositorio
    {
        public List<Producto> ObtenerTodos()
        {
            const string sql = @"
                SELECT id, codigo, nombre, descripcion, precio, stock
                FROM productos
                ORDER BY nombre;";

            var productos = new List<Producto>();
            using var connection = new MySqlConnection(ConexionDb.CadenaConexion);
            connection.Open();

            using var command = new MySqlCommand(sql, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                productos.Add(new Producto
                {
                    Id = reader.GetInt32("id"),
                    Codigo = reader.GetString("codigo"),
                    Nombre = reader.GetString("nombre"),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? "" : reader.GetString("descripcion"),
                    Precio = reader.GetDecimal("precio"),
                    Stock = reader.GetInt32("stock")
                });
            }
            return productos;
        }
        public List<Producto> Buscar(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return ObtenerTodos();
            }
            const string sql = @"
                SELECT id, codigo, nombre, descripcion, precio, stock
                FROM productos
                WHERE codigo LIKE @termino OR nombre LIKE @termino
                ORDER BY nombre;";

            var productos = new List<Producto>();
            using var connection = new MySqlConnection(ConexionDb.CadenaConexion);
            connection.Open();

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@termino", $"%{texto}%");
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                productos.Add(new Producto
                {
                    Id = reader.GetInt32("id"),
                    Codigo = reader.GetString("codigo"),
                    Nombre = reader.GetString("nombre"),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? "" : reader.GetString("descripcion"),
                    Precio = reader.GetDecimal("precio"),
                    Stock = reader.GetInt32("stock")
                });
            }
            return productos;
        }

        public void Agregar(Producto p)
        {
            const string sql = @"
                INSERT INTO productos (codigo, nombre, descripcion, precio, stock)
                VALUES (@codigo, @nombre, @descripcion, @precio, @stock);
                SELECT LAST_INSERT_ID();";

            using var connection = new MySqlConnection(ConexionDb.CadenaConexion);
            connection.Open();

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@codigo", p.Codigo);
            command.Parameters.AddWithValue("@nombre", p.Nombre);
            command.Parameters.AddWithValue("@descripcion", p.Descripcion);
            command.Parameters.AddWithValue("@precio", p.Precio);
            command.Parameters.AddWithValue("@stock", p.Stock);

            p.Id = Convert.ToInt32(command.ExecuteScalar());
        }
        public void Actualizar(Producto p)
        {
            const string sql = @"
                UPDATE productos
                SET codigo = @codigo,
                    nombre = @nombre,
                    descripcion = @descripcion,
                    precio = @precio,
                    stock = @stock
                WHERE id = @id;";

            using var connection = new MySqlConnection(ConexionDb.CadenaConexion);
            connection.Open();

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", p.Id);
            command.Parameters.AddWithValue("@codigo", p.Codigo);
            command.Parameters.AddWithValue("@nombre", p.Nombre);
            command.Parameters.AddWithValue("@descripcion", p.Descripcion);
            command.Parameters.AddWithValue("@precio", p.Precio);
            command.Parameters.AddWithValue("@stock", p.Stock);
            command.ExecuteNonQuery();
        }
        public void Eliminar(int id)
        {
            const string sql = "DELETE FROM productos WHERE id = @id;";

            using var connection = new MySqlConnection(ConexionDb.CadenaConexion);
            connection.Open();

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
        public void DescontarStock(int productoId, int cantidad)
        {
            const string sql = @"
                UPDATE productos
                SET stock = stock - @cantidad
                WHERE id = @productoId;";

            using var connection = new MySqlConnection(ConexionDb.CadenaConexion);
            connection.Open();

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@cantidad", cantidad);
            command.Parameters.AddWithValue("@productoId", productoId);
            command.ExecuteNonQuery();
        }
    }
}
