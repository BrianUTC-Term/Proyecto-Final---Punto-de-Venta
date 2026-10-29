using System.Collections.Generic;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Datos
{
    // Contrato de acceso a datos de ventas.
    // Para conectar la base de datos real: crea una clase que implemente esta interfaz
    // (ej. VentaRepositorioMySql) y registrala en RepositorioFactory.
    public interface IVentaRepositorio
    {
        void Registrar(VentaCabecera venta);
        List<VentaDetalle> ObtenerDetallesVendidos();
        List<VentaCabecera> ObtenerVentasConCliente();
    }
}
