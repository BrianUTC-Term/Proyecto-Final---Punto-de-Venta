namespace WinFormsApp1.Modelos
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Correo { get; set; } = "";

        public override string ToString() => Nombre;
    }
}
