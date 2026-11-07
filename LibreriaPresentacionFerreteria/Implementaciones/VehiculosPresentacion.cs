using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public class VehiculosPresentacion : IVehiculosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public VehiculosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Vehiculos Actualizar(Vehiculos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Vehiculos>(
                "Vehiculos/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Vehiculos Borrar(Vehiculos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Vehiculos>(
                "Vehiculos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Vehiculos> Buscar(Vehiculos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Vehiculos>>(
                "Vehiculos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Vehiculos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Vehiculos>>(
                "Vehiculos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Vehiculos Insertar(Vehiculos entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Vehiculos>(
                "Vehiculos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Vehiculos entidad)
        {
            return string.IsNullOrEmpty(entidad.matricula) ||
                   string.IsNullOrEmpty(entidad.marca) ||
                   string.IsNullOrEmpty(entidad.numeroDeChasis) ||
                    string.IsNullOrEmpty(entidad.numeroMotor);
        }
    }
}