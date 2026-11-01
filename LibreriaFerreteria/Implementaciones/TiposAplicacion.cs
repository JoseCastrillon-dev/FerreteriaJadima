using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaFerreteria.Interfaces
{
    public class TiposAplicacion : ITiposAplicacion
    {
        private IConexion IConexion;

        public TiposAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Tipos Actualizar(Tipos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Tipos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Tipos Borrar(Tipos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Tipos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Tipos> Buscar(Tipos entidad)
        {
            return this.IConexion.Tipos!
                .Where(x => x.nombre!.Contains(entidad.nombre!)).ToList();
        }

        public List<Tipos> Consultar()
        {
            return this.IConexion.Tipos!.ToList();
        }

        public Tipos Insertar(Tipos entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Tipos!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Tipos entidad)
        {
            return
                string.IsNullOrEmpty(entidad.nombre);
        }
    }
}