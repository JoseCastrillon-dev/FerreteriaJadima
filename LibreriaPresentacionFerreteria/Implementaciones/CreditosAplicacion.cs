using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaFerreteria.Interfaces
{
    public class CreditosPresentacion: ICreditosAplicacion
    {
        private IConexion IConexion;

        public CreditosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Creditos Actualizar(Creditos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var entry = this.IConexion!.Entry<Creditos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Creditos Borrar(Creditos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Creditos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Creditos> Buscar(Creditos entidad)
        {
            return this.IConexion.Creditos!
                .Include(x => x._venta).Include(x => x._estadoCredito)
                .Where(x => x.idVenta == entidad.idVenta).ToList();
        }

        public List<Creditos> Consultar()
        {
            return this.IConexion.Creditos!.ToList();
        }

        public Creditos Insertar(Creditos entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            this.IConexion.Creditos!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }


    }
}