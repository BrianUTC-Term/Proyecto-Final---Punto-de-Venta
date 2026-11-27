using System.Collections.Generic;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Datos
{
    // Contrato de acceso a datos de clientes.
    public interface IClienteRepositorio
    {
        List<Cliente> ObtenerTodos();
        List<Cliente> Buscar(string texto);
        int Agregar(Cliente c);
        void Actualizar(Cliente c);
        void Eliminar(int id);
    }
}
