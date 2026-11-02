using LibreriaFerreteria.Entidades;

namespace LibreriaFerreteria.Interfaces
{
    public interface IDetalleVentasAplicacion
    {
        DetalleVentas Insertar(DetalleVentas entidad);
        DetalleVentas Actualizar(DetalleVentas entidad);
        List<DetalleVentas> Consultar();
        List<DetalleVentas> Buscar(DetalleVentas entidad);
        DetalleVentas Borrar(DetalleVentas entidad);
    }
}