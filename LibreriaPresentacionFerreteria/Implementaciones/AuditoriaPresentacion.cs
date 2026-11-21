using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public class AuditoriaPresentacion : IAuditoriaPresentacion
    {
        private IComunicaciones IComunicaciones;

        public AuditoriaPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Auditoria Actualizar(Auditoria entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Auditoria>(
                "Auditoria/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Auditoria Borrar(Auditoria entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Auditoria>(
                "Auditoria/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Auditoria> Buscar(Auditoria entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Auditoria>>(
                "Auditoria/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Auditoria> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Auditoria>>(
                "Auditoria/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Auditoria Insertar(Auditoria entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Auditoria>(
                "Auditoria/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Auditoria entidad)
        {
            return string.IsNullOrEmpty(entidad.tablaAfectada);
        }
    }
}