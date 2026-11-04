using LibreriaFerreteria.Entidades;

namespace LibreriaFerreteria.Interfaces
{
    public interface IDetalleComprasAplicacion
    {
        DetalleCompras Insertar(DetalleCompras entidad);
        DetalleCompras Actualizar(DetalleCompras entidad);
        List<DetalleCompras> Consultar();
        List<DetalleCompras> Buscar(DetalleCompras entidad);
        DetalleCompras Borrar(DetalleCompras entidad);
    }
}