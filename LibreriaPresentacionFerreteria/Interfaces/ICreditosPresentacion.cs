using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public interface ICreditosPresentacion
    {
        Creditos Insertar(Creditos entidad);
        Creditos Actualizar(Creditos entidad);
        List<Creditos> Consultar();
        List<Creditos> Buscar(Creditos entidad);
        Creditos Borrar(Creditos entidad);
    }
}