using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class DomiciliosAplicacion : IDomiciliosAplicacion
    {
        private IConexion IConexion;

        public DomiciliosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Domicilios Actualizar(Domicilios entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Domicilios>(entidad);
            entry.State = EntityState.Modified;
            var Auditoria = new Auditoria
            {
                Desc = "Se Actualizo ",
                fecha = DateTime.Now,
                tablaAfectada = "Domicilios"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Domicilios Borrar(Domicilios entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Domicilios!.Remove(entidad!);
            var Auditoria = new Auditoria
            {
                Desc = "Se Borro ",
                fecha = DateTime.Now,
                tablaAfectada = "Domicilios"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Domicilios> Buscar(Domicilios entidad)
        {
            var Auditoria = new Auditoria
            {
                Desc = "Se Busco ",
                fecha = DateTime.Now,
                tablaAfectada = "Domicilios"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return this.IConexion.Domicilios!
                .Include(x => x._venta)
                .Include(x => x._vehiculos)
                .Include(x => x._venta._detalleVenta)
                .Include(x => x._venta._detalleVenta._producto)
                .Where(x => x.direccion!.Contains(entidad.direccion!))
                .ToList();
        }

        public List<Domicilios> Consultar()
        {
            var Auditoria = new Auditoria
            {
                Desc = "Se Consulto ",
                fecha = DateTime.Now,
                tablaAfectada = "Domicilios"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return this.IConexion.Domicilios!.ToList();
        }

        public Domicilios Insertar(Domicilios entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Domicilios!.Add(entidad!);
            var Auditoria = new Auditoria
            {
                Desc = "Se Inserto ",
                fecha = DateTime.Now,
                tablaAfectada = "Domicilios"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Domicilios entidad)
        {
            return
                string.IsNullOrEmpty(entidad.numeroDeTelefono) ||
                string.IsNullOrEmpty(entidad.direccion);
        }
    }
}