
using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public interface ICargosAplicacion
    {
        Cargos Insertar(Cargos entidad);
        Cargos Actualizar(Cargos entidad);
        List<Cargos> Consultar();
        List<Cargos> Buscar(Cargos entidad);
        Cargos Borrar(Cargos entidad);
    }
}