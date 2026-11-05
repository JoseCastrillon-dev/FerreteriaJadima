using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public interface IEntregasAplicacion
    {
        Entregas Insertar(Entregas entidad);
        Entregas Actualizar(Entregas entidad);
        List<Entregas> Consultar();
        List<Entregas> Buscar(Entregas entidad);
        Entregas Borrar(Entregas entidad);
    }
}