using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaFerreteria.Interfaces
{
    public class TiposDePagosAplicacioncs : ITiposDePagosAplicacion
    {
        private IConexion IConexion;

        public TiposDePagosAplicacioncs(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public TiposDePagos Actualizar(TiposDePagos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<TiposDePagos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public TiposDePagos Borrar(TiposDePagos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.TiposDePagos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<TiposDePagos> Buscar(TiposDePagos entidad)
        {
            return this.IConexion.TiposDePagos!
                .Where(x => x.metodo!.Contains(entidad.metodo!)).ToList();
        }

        public List<TiposDePagos> Consultar()
        {
            return this.IConexion.TiposDePagos!.ToList();
        }

        public TiposDePagos Insertar(TiposDePagos entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.TiposDePagos!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(TiposDePagos entidad)
        {
            return
                string.IsNullOrEmpty(entidad.metodo);
        }
    }
}