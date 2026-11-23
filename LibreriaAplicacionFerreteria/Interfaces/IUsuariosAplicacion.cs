
using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public interface IUsuariosAplicacion
    {
        Usuarios Ingresar(Usuarios entidad);
        Usuarios Actualizar(Usuarios entidad);
        List<Usuarios> Consultar();
        List<Usuarios> Buscar(Usuarios entidad);
        Usuarios Insertar(Usuarios entidad);
        Usuarios Borrar(Usuarios entidad);
    }
}