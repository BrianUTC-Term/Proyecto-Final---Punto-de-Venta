namespace WinFormsApp1.Modelos
{
    public class Producto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public override string ToString() => $"{Codigo} - {Nombre}";
    }
}
