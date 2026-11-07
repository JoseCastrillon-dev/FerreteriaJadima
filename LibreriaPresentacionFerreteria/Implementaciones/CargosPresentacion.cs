using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public class CargosPresentacion : ICargosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public CargosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Cargos Actualizar(Cargos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Cargos>(
                "Cargos/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Cargos Borrar(Cargos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Cargos>(
                "Cargos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Cargos> Buscar(Cargos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Cargos>>(
                "Cargos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Cargos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Cargos>>(
                "Cargos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Cargos Insertar(Cargos entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Cargos>(
                "Cargos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Cargos entidad)
        {
            return string.IsNullOrEmpty(entidad.nombre);
        }
    }
}