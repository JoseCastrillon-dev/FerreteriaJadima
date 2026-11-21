using LibreriaAplicacionFerreteria.Entidades;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public interface IAuditoriaPresentacion
    {
        Auditoria Insertar(Auditoria entidad);
        Auditoria Actualizar(Auditoria entidad);
        List<Auditoria> Consultar();
        List<Auditoria> Buscar(Auditoria entidad);
        Auditoria Borrar(Auditoria entidad);
    }
}