using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class TiposDePagosAplicacion : ITiposDePagosAplicacion
    {
        private IConexion IConexion;

        public TiposDePagosAplicacion(IConexion IConexion)
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
            var Auditoria = new Auditoria
            {
                Desc = "Se Actualizo ",
                fecha = DateTime.Now,
                tablaAfectada = "TiposDePagos"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public TiposDePagos Borrar(TiposDePagos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.TiposDePagos!.Remove(entidad!);
            var Auditoria = new Auditoria
            {
                Desc = "Se Borro ",
                fecha = DateTime.Now,
                tablaAfectada = "TiposDePagos"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<TiposDePagos> Buscar(TiposDePagos entidad)
        {
            var Auditoria = new Auditoria
            {
                Desc = "Se Busco ",
                fecha = DateTime.Now,
                tablaAfectada = "TiposDePagos"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return this.IConexion.TiposDePagos!
                .Where(x => x.metodo!.Contains(entidad.metodo!)).ToList();
        }

        public List<TiposDePagos> Consultar()
        {
            var Auditoria = new Auditoria
            {
                Desc = "Se Consulto ",
                fecha = DateTime.Now,
                tablaAfectada = "TiposDePagos"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return this.IConexion.TiposDePagos!.ToList();
        }

        public TiposDePagos Insertar(TiposDePagos entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.TiposDePagos!.Add(entidad!);
            var Auditoria = new Auditoria
            {
                Desc = "Se Inserto ",
                fecha = DateTime.Now,
                tablaAfectada = "TiposDePagos"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
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