using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public interface IEstadoCreditosPresentacion
    {
        EstadoCreditos Insertar(EstadoCreditos entidad);
        EstadoCreditos Actualizar(EstadoCreditos entidad);
        List<EstadoCreditos> Consultar();
        List<EstadoCreditos> Buscar(EstadoCreditos entidad);
        EstadoCreditos Borrar(EstadoCreditos entidad);
    }
}