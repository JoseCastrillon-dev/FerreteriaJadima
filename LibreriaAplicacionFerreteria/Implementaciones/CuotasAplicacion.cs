using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class CuotasAplicacion : ICuotasAplicacion
    {
        private IConexion IConexion;

        public CuotasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Cuotas Actualizar(Cuotas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");


            var entry = this.IConexion!.Entry<Cuotas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Cuotas Borrar(Cuotas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Cuotas!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Cuotas> Buscar(Cuotas entidad)
        {
            return this.IConexion.Cuotas!
                .Include(x => x._credito).Include(y => y._tipoDePago)
                .Where(x => x.id_credito == entidad.id_credito).ToList();
        }

        public List<Cuotas> Consultar()
        {
            return this.IConexion.Cuotas!.ToList();
        }

        public Cuotas Insertar(Cuotas entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            this.IConexion.Cuotas!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }


    }
}