using LibreriaFerreteria.Entidades;

namespace LibreriaFerreteria.Interfaces
{
    public interface ITiposDePagosAplicacion
    {
        TiposDePagos Insertar(TiposDePagos entidad);
        TiposDePagos Actualizar(TiposDePagos entidad);
        List<TiposDePagos> Consultar();
        List<TiposDePagos> Buscar(TiposDePagos entidad);
        TiposDePagos Borrar(TiposDePagos entidad);
    }
}