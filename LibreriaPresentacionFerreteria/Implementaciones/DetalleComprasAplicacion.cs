using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaFerreteria.Interfaces
{
    public class DetalleComprasPresentacion: IDetalleComprasAplicacion
    {
        private IConexion IConexion;

        public DetalleComprasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public DetalleCompras Actualizar(DetalleCompras entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var entry = this.IConexion!.Entry<DetalleCompras>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public DetalleCompras Borrar(DetalleCompras entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.DetalleCompras!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<DetalleCompras> Buscar(DetalleCompras entidad)
        {
            return this.IConexion.DetalleCompras!
                .Include(x => x._producto).Include(x => x._compra)
                .Where(x => x.id_compra == entidad.id_compra).ToList();
        }

        public List<DetalleCompras> Consultar()
        {
            return this.IConexion.DetalleCompras!.ToList();
        }

        public DetalleCompras Insertar(DetalleCompras entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            this.IConexion.DetalleCompras!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

    }
}