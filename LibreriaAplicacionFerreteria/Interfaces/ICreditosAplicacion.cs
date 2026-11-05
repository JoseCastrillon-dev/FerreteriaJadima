using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
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