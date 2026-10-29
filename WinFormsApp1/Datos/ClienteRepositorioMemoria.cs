using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Datos
{
    // Simulacion en memoria (lista) mientras no se conecta la base de datos real.
    public class ClienteRepositorioMemoria : IClienteRepositorio
    {
        private readonly List<Cliente> clientes = new()
        {
            new Cliente { Id = 1, Nombre = "Cliente Mostrador", Telefono = "0000-0000", Correo = "mostrador@correo.com" }
        };
        private int siguienteId = 2;

        public List<Cliente> ObtenerTodos() =>
            clientes.OrderBy(c => c.Nombre).ToList();

        public List<Cliente> Buscar(string texto) =>
            clientes
                .Where(c => c.Nombre.Contains(texto, StringComparison.OrdinalIgnoreCase))
                .OrderBy(c => c.Nombre)
                .ToList();

        public int Agregar(Cliente c)
        {
            c.Id = siguienteId++;
            clientes.Add(c);
            return c.Id;
        }

        public void Actualizar(Cliente c)
        {
            var existente = clientes.FirstOrDefault(x => x.Id == c.Id);
            if (existente == null) return;
            existente.Nombre = c.Nombre;
            existente.Telefono = c.Telefono;
            existente.Correo = c.Correo;
        }

        public void Eliminar(int id)
        {
            clientes.RemoveAll(c => c.Id == id);
        }
    }
}
