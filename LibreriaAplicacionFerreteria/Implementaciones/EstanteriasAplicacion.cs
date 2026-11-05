using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class EstanteriasAplicacion : IEstanteriasAplicacion
    {
        private IConexion IConexion;

        public EstanteriasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Estanterias Actualizar(Estanterias entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Estanterias>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Estanterias Borrar(Estanterias entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Estanterias!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Estanterias> Buscar(Estanterias entidad)
        {
            return this.IConexion.Estanterias!
                .Include(x => x._empleado).Include(y => y._producto)
                .Where(x => x.codigo!.Contains(entidad.codigo!)).ToList();
        }

        public List<Estanterias> Consultar()
        {
            return this.IConexion.Estanterias!.ToList();
        }

        public Estanterias Insertar(Estanterias entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Estanterias!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Estanterias entidad)
        {
            return
                string.IsNullOrEmpty(entidad.codigo);
        }
    }
}