using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibreriaFerreteria.Implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Cargos>? Cargos { get; set; }
        public DbSet<Clientes>? Clientes { get; set; }
        public DbSet<Compras>? Compras { get; set; }
        public DbSet<Creditos>? Creditos { get; set; }
        public DbSet<Cuotas>? Cuotas { get; set; }
        public DbSet<DetalleCompras>? DetalleCompras { get; set; }
        public DbSet<DetalleVentas>? DetalleVentas { get; set; }
        public DbSet<Domicilios>? Domicilios { get; set; }
        public DbSet<Empleados>? Empleados { get; set; }
        public DbSet<Entregas>? Entregas { get; set; }
        public DbSet<EstadoCreditos>? EstadoCreditos { get; set; }
        public DbSet<Estanterias>? Estanterias { get; set; }
        public DbSet<Personas>? Personas { get; set; }
        public DbSet<Productos>? Productos { get; set; }
        public DbSet<Proveedores>? Proveedores { get; set; }
        public DbSet<Salarios>? Salarios { get; set; }
        public DbSet<Tipos>? Tipos { get; set; }
        public DbSet<TiposDePagos>? TiposDePagos { get; set; }
        public DbSet<Vehiculos>? Vehiculos { get; set; }
        public DbSet<Ventas>? Ventas { get; set; }
    }
}
