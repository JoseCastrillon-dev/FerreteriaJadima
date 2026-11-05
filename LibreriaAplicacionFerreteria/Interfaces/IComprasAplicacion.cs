using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public interface IComprasAplicacion
    {
        Compras Insertar(Compras entidad);
        Compras Actualizar(Compras entidad);
        List<Compras> Consultar();
        List<Compras> Buscar(Compras entidad);
        Compras Borrar(Compras entidad);
    }
}