using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
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
