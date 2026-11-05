using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public interface ISalariosAplicacion
    {
        Salarios Insertar(Salarios entidad);
        Salarios Actualizar(Salarios entidad);
        List<Salarios> Consultar();
        List<Salarios> Buscar(Salarios entidad);
        Salarios Borrar(Salarios entidad);
    }
}