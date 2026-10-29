using System;

namespace SistemaVentas.Entidades;

/// <summary>
/// Representa un producto del sistema de ventas.
/// </summary>
public class Producto
{
    private string _codigo = string.Empty;
    private string _nombre = string.Empty;
    private decimal _precio;
    private int _stock;

    /// <summary>
    /// Código único del producto.
    /// </summary>
    /// <exception cref="ArgumentException">Si el código es nulo o vacío.</exception>
    public string Codigo
    {
        get => _codigo;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El código del producto es obligatorio.");
            _codigo = value.Trim();
        }
    }

    /// <summary>
    /// Nombre del producto.
    /// </summary>
    /// <exception cref="ArgumentException">Si el nombre es nulo o vacío.</exception>
    public string Nombre
    {
        get => _nombre;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El nombre del producto es obligatorio.");
            _nombre = value.Trim();
        }
    }

    /// <summary>
    /// Descripción del producto (opcional).
    /// </summary>
    public string Descripcion { get; set; } = string.Empty;

    /// <summary>
    /// Precio unitario del producto.
    /// </summary>
    /// <exception cref="ArgumentException">Si el precio es negativo.</exception>
    public decimal Precio
    {
        get => _precio;
        set
        {
            if (value < 0)
                throw new ArgumentException("El precio no puede ser negativo.");
            _precio = value;
        }
    }

    /// <summary>
    /// Cantidad disponible en inventario.
    /// </summary>
    /// <exception cref="ArgumentException">Si el stock es negativo.</exception>
    public int Stock
    {
        get => _stock;
        set
        {
            if (value < 0)
                throw new ArgumentException("El stock no puede ser negativo.");
            _stock = value;
        }
    }

    /// <summary>
    /// Constructor vacío (útil para DAO, DTO y serialización JSON).
    /// </summary>
    public Producto() { }

    /// <summary>
    /// Crea un producto con todos sus datos.
    /// </summary>
    /// <param name="codigo">Código único.</param>
    /// <param name="nombre">Nombre del producto.</param>
    /// <param name="descripcion">Descripción del producto.</param>
    /// <param name="precio">Precio unitario.</param>
    /// <param name="stock">Cantidad en inventario.</param>
    public Producto(string codigo, string nombre, string descripcion, decimal precio, int stock)
    {
        Codigo = codigo;
        Nombre = nombre;
        Descripcion = descripcion;
        Precio = precio;
        Stock = stock;
    }

    /// <summary>
    /// Aumenta el stock (por ejemplo, al recibir mercadería).
    /// </summary>
    /// <param name="cantidad">Cantidad a agregar, mayor que cero.</param>
    public void AumentarStock(int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad debe ser mayor que cero.");
        Stock += cantidad;
    }

    /// <summary>
    /// Disminuye el stock (por ejemplo, al registrar una venta).
    /// </summary>
    /// <param name="cantidad">Cantidad a descontar, mayor que cero.</param>
    /// <exception cref="InvalidOperationException">Si no hay stock suficiente.</exception>
    public void ReducirStock(int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad debe ser mayor que cero.");
        if (cantidad > Stock)
            throw new InvalidOperationException($"Stock insuficiente. Disponible: {Stock}.");
        Stock -= cantidad;
    }

    /// <summary>
    /// Indica si hay stock suficiente para la cantidad solicitada.
    /// </summary>
    public bool HayStock(int cantidad) => cantidad > 0 && cantidad <= Stock;

    /// <summary>
    /// Representación en texto del producto.
    /// </summary>
    public override string ToString() =>
        $"[{Codigo}] {Nombre} - ₡{Precio:N2} (Stock: {Stock})";
}
