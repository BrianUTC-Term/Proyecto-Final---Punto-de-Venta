
using System;

namespace SistemaVentas
{
    public class Venta
    {
        // Propiedades de la venta
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public decimal Total { get; set; }

        // Constructor
        public Venta()
        {
            Fecha = DateTime.Now;
            Total = 0;
        }

        // Metodo para mostrar la informacion de la venta
        public void MostrarInfo()
        {
            Console.WriteLine("ID de venta: " + Id);
            Console.WriteLine("Fecha: " + Fecha);
            Console.WriteLine("ID del cliente: " + IdCliente);
            Console.WriteLine("Total de la venta: " + Total);
        }
    }
}
