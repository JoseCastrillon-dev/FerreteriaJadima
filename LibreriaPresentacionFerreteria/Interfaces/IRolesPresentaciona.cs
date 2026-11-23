using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public interface IRolesPresentacion
    {
        Roles Actualizar(Roles entidad);
        List<Roles> Consultar();
        List<Roles> Buscar(Roles entidad);
        Roles Insertar(Roles entidad);
        Roles Borrar(Roles entidad);
    }
}