using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public interface ISalariosPresentacion
    {
        Salarios Insertar(Salarios entidad);
        Salarios Actualizar(Salarios entidad);
        List<Salarios> Consultar();
        List<Salarios> Buscar(Salarios entidad);
        Salarios Borrar(Salarios entidad);
    }
}