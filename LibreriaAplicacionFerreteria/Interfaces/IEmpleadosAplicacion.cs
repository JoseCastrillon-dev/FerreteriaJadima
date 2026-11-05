using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public interface IEmpleadosAplicacion
    {
        Empleados Insertar(Empleados entidad);
        Empleados Actualizar(Empleados entidad);
        List<Empleados> Consultar();
        List<Empleados> Buscar(Empleados entidad);
        Empleados Borrar(Empleados entidad);
    }
}