using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public interface ITiposDePagosPresentacion
    {
        TiposDePagos Insertar(TiposDePagos entidad);
        TiposDePagos Actualizar(TiposDePagos entidad);
        List<TiposDePagos> Consultar();
        List<TiposDePagos> Buscar(TiposDePagos entidad);
        TiposDePagos Borrar(TiposDePagos entidad);
    }
}