using LibreriaAplicacionFerreteria.Entidades;

namespace   LibreriaPresentacionFerreteria.Interfaces
{
    public interface IDomiciliosPresentacion
    {
        Domicilios Insertar(Domicilios entidad);
        Domicilios Actualizar(Domicilios entidad);
        List<Domicilios> Consultar();
        List<Domicilios> Buscar(Domicilios entidad);
        Domicilios Borrar(Domicilios entidad);
    }
}