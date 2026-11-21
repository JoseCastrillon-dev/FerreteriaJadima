using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class DetalleVentasAplicacion : IDetalleVentasAplicacion
    {
        private IConexion IConexion;

        public DetalleVentasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public DetalleVentas Actualizar(DetalleVentas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var entry = this.IConexion!.Entry<DetalleVentas>(entidad);
            entry.State = EntityState.Modified;
            var Auditoria = new Auditoria
            {
                Desc = "Se Actualizo ",
                fecha = DateTime.Now,
                tablaAfectada = "DetalleVentas"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public DetalleVentas Borrar(DetalleVentas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.DetalleVentas!.Remove(entidad!);
            var Auditoria = new Auditoria
            {
                Desc = "Se Borro ",
                fecha = DateTime.Now,
                tablaAfectada = "DetalleVentas"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<DetalleVentas> Buscar(DetalleVentas entidad)
        {
            var Auditoria = new Auditoria
            {
                Desc = "Se Busco ",
                fecha = DateTime.Now,
                tablaAfectada = "DetalleVentas"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return this.IConexion.DetalleVentas!
                .Include(x => x._producto)
                .Where(x => x._producto!.codigo!.Contains(entidad._producto!.codigo!))
                .Take(10)
                .ToList();
        }

        public List<DetalleVentas> Consultar()
        {
            var Auditoria = new Auditoria
            {
                Desc = "Se Consulto ",
                fecha = DateTime.Now,
                tablaAfectada = "DetalleVentas"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return this.IConexion.DetalleVentas!.ToList();
        }

        public DetalleVentas Insertar(DetalleVentas entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            this.IConexion.DetalleVentas!.Add(entidad!);
            var Auditoria = new Auditoria
            {
                Desc = "Se Inserto ",
                fecha = DateTime.Now,
                tablaAfectada = "DetalleVentas"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }

    }
}