using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class VentasAplicacion : IVentasAplicacion
    {
        private IConexion IConexion;

        public VentasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Ventas Actualizar(Ventas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");


            var entry = this.IConexion!.Entry<Ventas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Ventas Borrar(Ventas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Ventas!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Ventas> Buscar(Ventas entidad)
        {
            return this.IConexion.Ventas!
                .Include(x => x._detalleVenta).Include(y => y._tipoDePago).Include(z => z._empleado)
                .Include(x2 => x2._cliente)
                .Where(x => x.id_detalleVenta == entidad.id_detalleVenta).ToList();
        }

        public List<Ventas> Consultar()
        {
            return this.IConexion.Ventas!.ToList();
        }

        public Ventas Insertar(Ventas entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            this.IConexion.Ventas!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }


    }
}