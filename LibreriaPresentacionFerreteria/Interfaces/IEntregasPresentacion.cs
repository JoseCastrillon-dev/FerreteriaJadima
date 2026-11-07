using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public interface IEntregasPresentacion
    {
        Entregas Insertar(Entregas entidad);
        Entregas Actualizar(Entregas entidad);
        List<Entregas> Consultar();
        List<Entregas> Buscar(Entregas entidad);
        Entregas Borrar(Entregas entidad);
    }
}