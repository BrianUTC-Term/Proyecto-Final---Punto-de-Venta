using System.Collections.Generic;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Datos
{
    public interface IVentaRepositorio
    {
        void Registrar(VentaCabecera venta);
        List<VentaDetalle> ObtenerDetallesVendidos();
        List<VentaCabecera> ObtenerVentasConCliente();
    }
}
