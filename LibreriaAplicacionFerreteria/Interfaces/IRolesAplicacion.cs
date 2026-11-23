
using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public interface IRolesAplicacion
    {
        List<Roles> Consultar();
        List<Roles> Buscar(Roles entidad);
        Roles Insertar(Roles entidad);
        Roles Borrar(Roles entidad);
    }
}