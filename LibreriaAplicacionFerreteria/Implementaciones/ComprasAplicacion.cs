using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class ComprasAplicacion : IComprasAplicacion
    {
        private IConexion IConexion;

        public ComprasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Compras Actualizar(Compras entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");


            var entry = this.IConexion!.Entry<Compras>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Compras Borrar(Compras entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Compras!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Compras> Buscar(Compras entidad)
        {
            return this.IConexion.Compras!
                .Include(x => x._empleado).Include(x => x._proveedor)
                .Where(x => x.id_empleado == entidad.id_empleado).ToList();
        }

        public List<Compras> Consultar()
        {
            return this.IConexion.Compras!.ToList();
        }

        public Compras Insertar(Compras entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            this.IConexion.Compras!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

    }
}