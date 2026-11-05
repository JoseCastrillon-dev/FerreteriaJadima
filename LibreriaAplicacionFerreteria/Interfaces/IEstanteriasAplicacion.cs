using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public interface IEstanteriasAplicacion
    {
        Estanterias Insertar(Estanterias entidad);
        Estanterias Actualizar(Estanterias entidad);
        List<Estanterias> Consultar();
        List<Estanterias> Buscar(Estanterias entidad);
        Estanterias Borrar(Estanterias entidad);
    }
}