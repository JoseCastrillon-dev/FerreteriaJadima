using LibreriaFerreteria.Entidades;

namespace LibreriaFerreteria.Interfaces
{
    public interface ITiposAplicacion
    {
        Tipos Insertar(Tipos entidad);
        Tipos Actualizar(Tipos entidad);
        List<Tipos> Consultar();
        List<Tipos> Buscar(Tipos entidad);
        Tipos Borrar(Tipos entidad);
    }
}