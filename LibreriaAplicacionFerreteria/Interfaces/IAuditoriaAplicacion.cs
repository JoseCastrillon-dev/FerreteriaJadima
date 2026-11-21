
using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public interface IAuditoriaAplicacion
    {
        Auditoria Insertar(Auditoria entidad);
        List<Auditoria> Consultar();
        List<Auditoria> Buscar(Auditoria entidad);
    }
}