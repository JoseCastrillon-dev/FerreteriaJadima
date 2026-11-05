using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public interface IEstadoCreditosAplicacion
    {
        EstadoCreditos Insertar(EstadoCreditos entidad);
        EstadoCreditos Actualizar(EstadoCreditos entidad);
        List<EstadoCreditos> Consultar();
        List<EstadoCreditos> Buscar(EstadoCreditos entidad);
        EstadoCreditos Borrar(EstadoCreditos entidad);
    }
}