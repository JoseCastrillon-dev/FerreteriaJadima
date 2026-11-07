using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public interface IDetalleComprasPresentacion
    {
        DetalleCompras Insertar(DetalleCompras entidad);
        DetalleCompras Actualizar(DetalleCompras entidad);
        List<DetalleCompras> Consultar();
        List<DetalleCompras> Buscar(DetalleCompras entidad);
        DetalleCompras Borrar(DetalleCompras entidad);
    }
}