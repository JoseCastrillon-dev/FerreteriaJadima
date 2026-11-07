using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public interface IDetalleVentasPresentacion
    {
        DetalleVentas Insertar(DetalleVentas entidad);
        DetalleVentas Actualizar(DetalleVentas entidad);
        List<DetalleVentas> Consultar();
        List<DetalleVentas> Buscar(DetalleVentas entidad);
        DetalleVentas Borrar(DetalleVentas entidad);
    }
}