using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public interface IPersonasAplicacion
    {
        Personas Insertar(Personas entidad);
        Personas Actualizar(Personas entidad);
        List<Personas> Consultar();
        List<Personas> Buscar(Personas entidad);
        Personas Borrar(Personas entidad);
    }
}