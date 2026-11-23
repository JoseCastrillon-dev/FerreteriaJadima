using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public interface IUsuariosPresentacion
    {
        Usuarios Ingresar(Usuarios entidad);
        Usuarios Actualizar(Usuarios entidad);
        List<Usuarios> Consultar();
        List<Usuarios> Buscar(Usuarios entidad);
        Usuarios Insertar(Usuarios entidad);
        Usuarios Borrar(Usuarios entidad);
    }
}