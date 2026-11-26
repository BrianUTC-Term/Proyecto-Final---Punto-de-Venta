using System;
using System.Collections.Generic;

namespace WinFormsApp1.Modelos
{
    public class VentaCabecera
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; } = "";
        public decimal Total { get; set; }
        public List<VentaDetalle> Detalles { get; set; } = new();
    }
}
