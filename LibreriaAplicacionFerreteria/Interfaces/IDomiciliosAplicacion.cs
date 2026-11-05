using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public interface IDomiciliosAplicacion
    {
        Domicilios Insertar(Domicilios entidad);
        Domicilios Actualizar(Domicilios entidad);
        List<Domicilios> Consultar();
        List<Domicilios> Buscar(Domicilios entidad);
        Domicilios Borrar(Domicilios entidad);
    }
}