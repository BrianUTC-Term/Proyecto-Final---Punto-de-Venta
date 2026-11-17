using System.Collections.Generic;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Datos
{
    // Contrato de acceso a datos de ventas.
    public interface IVentaRepositorio
    {
        void Registrar(VentaCabecera venta);
        List<VentaDetalle> ObtenerDetallesVendidos();
        List<VentaCabecera> ObtenerVentasConCliente();
    }
}
