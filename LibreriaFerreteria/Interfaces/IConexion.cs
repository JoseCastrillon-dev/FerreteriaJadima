using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LibreriaFerreteria.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        DbSet<Cargos>? Cargos { get; set; }
        DbSet<Clientes>? Clientes { get; set; }
        DbSet<Compras>? Compras { get; set; }
        DbSet<Creditos>? Creditos { get; set; }
        DbSet<Cuotas>? Cuotas { get; set; }
        DbSet<DetalleCompras>? DetalleCompras { get; set; }
        DbSet<DetalleVentas>? DetalleVentas { get; set; }
        DbSet<Domicilios>? Domicilios { get; set; }
        DbSet<Empleados>? Empleados { get; set; }
        DbSet<Entregas>? Entregas { get; set; }
        DbSet<EstadoCreditos>? EstadoCreditos { get; set; }
        DbSet<Estanterias>? Estanterias { get; set; }
        DbSet<Personas>? Personas { get; set; }
        DbSet<Productos>? Productos { get; set; }
        DbSet<Proveedores>? Proveedores { get; set; }
        DbSet<Salarios>? Salarios { get; set; }
        DbSet<Tipos>? Tipos { get; set; }
        DbSet<TiposDePagos>? TiposDePagos { get; set; }
        DbSet<Vehiculos>? Vehiculos { get; set; }
        DbSet<Ventas>? Ventas { get; set; }

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}