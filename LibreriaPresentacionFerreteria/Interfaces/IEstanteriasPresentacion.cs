using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public interface IEstanteriasPresentacion
    {
        Estanterias Insertar(Estanterias entidad);
        Estanterias Actualizar(Estanterias entidad);
        List<Estanterias> Consultar();
        List<Estanterias> Buscar(Estanterias entidad);
        Estanterias Borrar(Estanterias entidad);
    }
}