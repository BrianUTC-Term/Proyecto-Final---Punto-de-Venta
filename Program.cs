using SistemaVentas.Entidades;

var p = new Producto("P001", "Teclado", "Teclado mecánico USB", 15000m, 10);
Console.WriteLine(p);

p.ReducirStock(3);
Console.WriteLine($"Stock después de vender 3: {p.Stock}");

try
{
    p.ReducirStock(50); // más de lo disponible
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
