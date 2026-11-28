using System;

namespace SistemaVentas.Entidades;

public class Cliente
{
    private string _nombre = string.Empty;
    private string _telefono = string.Empty;
    private string _correo = string.Empty;

    public int Id { get; set; }

    public string Nombre
    {
        get => _nombre;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El nombre del cliente es obligatorio.");

            _nombre = value.Trim();
        }
    }

    public string Telefono
    {
        get => _telefono;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El teléfono es obligatorio.");

            _telefono = value.Trim();
        }
    }

    public string Correo
    {
     