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
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public DetalleVentas Borrar(DetalleVentas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.DetalleVentas!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<DetalleVentas> Buscar(DetalleVentas entidad)
        {
            return this.IConexion.DetalleVentas!
                .Include(x => x._producto)
                .Where(x => x.id_producto == entidad.id_producto).ToList();
        }

        public List<DetalleVentas> Consultar()
        {
            return this.IConexion.DetalleVentas!.ToList();
        }

        public DetalleVentas Insertar(DetalleVentas entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            this.IConexion.DetalleVentas!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

    }
}