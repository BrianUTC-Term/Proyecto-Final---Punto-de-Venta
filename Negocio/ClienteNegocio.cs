using System.Collections.Generic;
using WinFormsApp1.Datos;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Negocio
{
    public static class ClienteNegocio
    {
        public static List<Cliente> ObtenerTodos() => RepositorioFactory.Clientes.ObtenerTodos();

        public static List<Cliente> Buscar(string texto) => RepositorioFactory.Clientes.Buscar(texto);

        public static int Agregar(Cliente c)
        {
            Validar(c);
            return RepositorioFactory.Clientes.Agregar(c);
        }

        public static void Actualizar(Cliente c)
        {
            Validar(c);
            RepositorioFactory.Clientes.Actualizar(c);
        }

        public static void Eliminar(int id) => RepositorioFactory.Clientes.Eliminar(id);

        private static void Validar(Cliente c)
        {
            if (string.IsNullOrWhiteSpace(c.Nombre))
                throw new NegocioException("El nombre del cliente es obligatorio.");
        }
    }
}
