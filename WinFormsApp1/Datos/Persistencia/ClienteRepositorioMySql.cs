using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Datos.Persistencia
{
    public class ClienteRepositorioMySql : IClienteRepositorio
    {
        public List<Cliente> ObtenerTodos()
        {
            using var db = new PosDbContext();
            return db.Clientes
                .AsNoTracking()
                .OrderBy(c => c.Nombre)
                .ToList();
        }

        public List<Cliente> Buscar(string texto)
        {
            using var db = new PosDbContext();
            if (string.IsNullOrWhiteSpace(texto))
            {
                return ObtenerTodos();
            }

            var termino = $"%{texto}%";
            return db.Clientes
                .AsNoTracking()
                .Where(c => EF.Functions.Like(c.Nombre, termino))
                .OrderBy(c => c.Nombre)
                .ToList();
        }

        public int Agregar(Cliente c)
        {
            using var db = new PosDbContext();
            db.Clientes.Add(c);
            db.SaveChanges();
            return c.Id;
        }

        public void Actualizar(Cliente c)
        {
            using var db = new PosDbContext();
            var existente = db.Clientes.FirstOrDefault(x => x.Id == c.Id);
            if (existente == null) return;

            existente.Nombre = c.Nombre;
            existente.Telefono = c.Telefono;
            existente.Correo = c.Correo;

            db.SaveChanges();
        }

        public void Eliminar(int id)
        {
            using var db = new PosDbContext();
            var existente = db.Clientes.FirstOrDefault(c => c.Id == id);
            if (existente == null) return;

            db.Clientes.Remove(existente);
            db.SaveChanges();
        }
    }
}
