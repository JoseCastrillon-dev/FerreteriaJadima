using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaFerreteria.Interfaces
{
    public class VehiculosAplicacion : IVehiculosAplicacion
    {
        private IConexion IConexion;

        public VehiculosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Vehiculos Actualizar(Vehiculos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Vehiculos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Vehiculos Borrar(Vehiculos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Vehiculos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Vehiculos> Buscar(Vehiculos entidad)
        {
            return this.IConexion.Vehiculos!
                .Where(x => x.matricula!.Contains(entidad.matricula!)).ToList();
        }

        public List<Vehiculos> Consultar()
        {
            return this.IConexion.Vehiculos!.ToList();
        }

        public Vehiculos Insertar(Vehiculos entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Vehiculos!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        private bool Validar(Vehiculos entidad)
        {
            return
                string.IsNullOrEmpty(entidad.matricula);
                
        }
    }
}