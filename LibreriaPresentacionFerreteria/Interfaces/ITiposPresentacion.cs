using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public interface ITiposPresentacion
    {
        Tipos Insertar(Tipos entidad);
        Tipos Actualizar(Tipos entidad);
        List<Tipos> Consultar();
        List<Tipos> Buscar(Tipos entidad);
        Tipos Borrar(Tipos entidad);
    }
}