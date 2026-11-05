using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public interface ICuotasAplicacion
    {
        Cuotas Insertar(Cuotas entidad);
        Cuotas Actualizar(Cuotas entidad);
        List<Cuotas> Consultar();
        List<Cuotas> Buscar(Cuotas entidad);
        Cuotas Borrar(Cuotas entidad);
    }
}