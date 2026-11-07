using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public interface ICuotasPresentacion
    {
        Cuotas Insertar(Cuotas entidad);
        Cuotas Actualizar(Cuotas entidad);
        List<Cuotas> Consultar();
        List<Cuotas> Buscar(Cuotas entidad);
        Cuotas Borrar(Cuotas entidad);
    }
}