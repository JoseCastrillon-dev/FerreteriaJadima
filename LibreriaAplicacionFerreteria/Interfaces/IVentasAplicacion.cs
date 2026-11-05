using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public interface IVentasAplicacion
    {
        Ventas Insertar(Ventas entidad);
        Ventas Actualizar(Ventas entidad);
        List<Ventas> Consultar();
        List<Ventas> Buscar(Ventas entidad);
        Ventas Borrar(Ventas entidad);
    }
}