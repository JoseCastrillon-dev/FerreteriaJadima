using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class AuditoriaAplicacion : IAuditoriaAplicacion
    {
        private IConexion IConexion;

        public AuditoriaAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public List<Auditoria> Buscar(Auditoria entidad)
        {
            var text = entidad.Desc ?? string.Empty;

            return this.IConexion.Auditoria!
                .Where(x => x.tablaAfectada!.Contains(text) || x.Desc!.Contains(text))
                            .ToList();
        }

        public List<Auditoria> Consultar()
        {
            return this.IConexion.Auditoria!.ToList();
        }

        public Auditoria Insertar(Auditoria entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Auditoria!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Auditoria entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Desc);
        }
    }
}