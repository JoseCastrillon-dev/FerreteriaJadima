using LibreriaFerreteria.Entidades;

namespace LibreriaFerreteria.Interfaces
{
    public interface ICreditosAplicacion
    {
        Creditos Insertar(Creditos entidad);
        Creditos Actualizar(Creditos entidad);
        List<Creditos> Consultar();
        List<Creditos> Buscar(Creditos entidad);
        Creditos Borrar(Creditos entidad);
    }
}