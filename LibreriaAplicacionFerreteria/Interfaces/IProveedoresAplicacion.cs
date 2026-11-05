using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public interface IProveedoresAplicacion
    {
        Proveedores Insertar(Proveedores entidad);
        Proveedores Actualizar(Proveedores entidad);
        List<Proveedores> Consultar();
        List<Proveedores> Buscar(Proveedores entidad);
        Proveedores Borrar(Proveedores entidad);
    }
}