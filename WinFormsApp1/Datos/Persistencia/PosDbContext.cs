using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Modelos;

namespace WinFormsApp1.Datos.Persistencia
{
    public class PosDbContext : DbContext
    {
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<VentaCabecera> VentasCabecera => Set<VentaCabecera>();
        public DbSet<VentaDetalle> VentasDetalle => Set<VentaDetalle>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    ConexionDb.CadenaConexion,
                    ServerVersion.AutoDetect(ConexionDb.CadenaConexion));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("productos");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Codigo).HasColumnName("codigo").HasMaxLength(20).IsRequired();
                entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(255);
                entity.Property(e => e.Precio).HasColumnName("precio").HasPrecision(10, 2);
                entity.Property(e => e.Stock).HasColumnName("stock");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("clientes");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Telefono).HasColumnName("telefono").HasMaxLength(20);
                entity.Property(e => e.Correo).HasColumnName("correo").HasMaxLength(100);
            });

            modelBuilder.Entity<VentaCabecera>(entity =>
            {
                entity.ToTable("venta_cabecera");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Fecha).HasColumnName("fecha").IsRequired();
                entity.Property(e => e.ClienteId).HasColumnName("cliente_id").IsRequired();
                entity.Property(e => e.Total).HasColumnName("total").HasPrecision(10, 2).IsRequired();
                entity.Ignore(e => e.ClienteNombre);

                entity.HasMany(e => e.Detalles)
                    .WithOne()
                    .HasForeignKey(d => d.VentaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<VentaDetalle>(entity =>
            {
                entity.ToTable("venta_detalle");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.VentaId).HasColumnName("venta_id").IsRequired();
                entity.Property(e => e.ProductoId).HasColumnName("producto_id").IsRequired();
                entity.Property(e => e.Cantidad).HasColumnName("cantidad").IsRequired();
                entity.Property(e => e.PrecioUnitario).HasColumnName("precio_unitario").HasPrecision(10, 2).IsRequired();
                entity.Ignore(e => e.ProductoNombre);
                entity.Ignore(e => e.Subtotal);
            });
        }
    }
}
