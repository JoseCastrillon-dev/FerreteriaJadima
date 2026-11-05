using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class EntregasAplicacion : IEntregasAplicacion
    {
        private IConexion IConexion;

        public EntregasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Entregas Actualizar(Entregas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");


            var entry = this.IConexion!.Entry<Entregas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Entregas Borrar(Entregas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Entregas!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Entregas> Buscar(Entregas entidad)
        {
            return this.IConexion.Entregas!
                .Include(x => x._domicilio).Include(z => z._empleado)
                .Where(x => x.id_domicilio == entidad.id_domicilio).ToList();
        }

        public List<Entregas> Consultar()
        {
            return this.IConexion.Entregas!.ToList();
        }

        public Entregas Insertar(Entregas entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            this.IConexion.Entregas!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }


    }
}