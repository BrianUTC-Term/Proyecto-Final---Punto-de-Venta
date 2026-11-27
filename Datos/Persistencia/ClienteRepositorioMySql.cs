using MySql.Data.MySqlClient;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Datos.Persistencia
{
    public class ClienteRepositorioMySql : IClienteRepositorio
    {
        public List<Cliente> ObtenerTodos()
        {
            const string sql = @"
                SELECT id, nombre, telefono, correo
                FROM clientes
                ORDER BY nombre;";

            var clientes = new List<Cliente>();
            using var connection = new MySqlConnection(ConexionDb.CadenaConexion);
            connection.Open();

            using var command = new MySqlCommand(sql, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                clientes.Add(new Cliente
                {
                    Id = reader.GetInt32("id"),
                    Nombre = reader.GetString("nombre"),
                    Telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? "" : reader.GetString("telefono"),
                    Correo = reader.IsDBNull(reader.GetOrdinal("correo")) ? "" : reader.GetString("correo")
                });
            }

            return clientes;
        }

        public List<Cliente> Buscar(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return ObtenerTodos();
            }

            const string sql = @"
                SELECT id, nombre, telefono, correo
                FROM clientes
                WHERE nombre LIKE @termino
                ORDER BY nombre;";

            var clientes = new List<Cliente>();
            using var connection = new MySqlConnection(ConexionDb.CadenaConexion);
            connection.Open();

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@termino", $"%{texto}%");
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                clientes.Add(new Cliente
                {
                    Id = reader.GetInt32("id"),
                    Nombre = reader.GetString("nombre"),
                    Telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? "" : reader.GetString("telefono"),
                    Correo = reader.IsDBNull(reader.GetOrdinal("correo")) ? "" : reader.GetString("correo")
                });
            }

            return clientes;
        }

        public int Agregar(Cliente c)
        {
            const string sql = @"
                INSERT INTO clientes (nombre, telefono, correo)
                VALUES (@nombre, @telefono, @correo);
                SELECT LAST_INSERT_ID();";

            using var connection = new MySqlConnection(ConexionDb.CadenaConexion);
            connection.Open();

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@nombre", c.Nombre);
            command.Parameters.AddWithValue("@telefono", c.Telefono);
            command.Parameters.AddWithValue("@correo", c.Correo);
            c.Id = Convert.ToInt32(command.ExecuteScalar());
            return c.Id;
        }

        public void Actualizar(Cliente c)
        {
            const string sql = @"
                UPDATE clientes
                SET nombre = @nombre,
                    telefono = @telefono,
                    correo = @correo
                WHERE id = @id;";

            using var connection = new MySqlConnection(ConexionDb.CadenaConexion);
            connection.Open();

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", c.Id);
            command.Parameters.AddWithValue("@nombre", c.Nombre);
            command.Parameters.AddWithValue("@telefono", c.Telefono);
            command.Parameters.AddWithValue("@correo", c.Correo);
            command.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            const string sql = "DELETE FROM clientes WHERE id = @id;";

            using var connection = new MySqlConnection(ConexionDb.CadenaConexion);
            connection.Open();

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}
