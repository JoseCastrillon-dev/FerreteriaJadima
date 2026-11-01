using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaFerreteria.Interfaces
{
    public class EstadoCreditosAplicacion : IEstadoCreditosAplicacion
    {
        private IConexion IConexion;

        public EstadoCreditosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public EstadoCreditos Actualizar(EstadoCreditos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<EstadoCreditos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public EstadoCreditos Borrar(EstadoCreditos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.EstadoCreditos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<EstadoCreditos> Buscar(EstadoCreditos entidad)
        {
            return this.IConexion.EstadoCreditos!
                .Where(x => x.estado!.Contains(entidad.estado!)
).ToList();
        }

        public List<EstadoCreditos> Consultar()
        {
            return this.IConexion.EstadoCreditos!.ToList();
        }

        public EstadoCreditos Insertar(EstadoCreditos entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.EstadoCreditos!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(EstadoCreditos entidad)
        {
            return
                string.IsNullOrEmpty(entidad.estado);
        }
    }
}