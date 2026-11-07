using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public class EstadoCreditosPresentacion : IEstadoCreditosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public EstadoCreditosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public EstadoCreditos Actualizar(EstadoCreditos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<EstadoCreditos>(
                "EstadoCreditos/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public EstadoCreditos Borrar(EstadoCreditos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<EstadoCreditos>(
                "EstadoCreditos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<EstadoCreditos> Buscar(EstadoCreditos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<EstadoCreditos>>(
                "EstadoCreditos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<EstadoCreditos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<EstadoCreditos>>(
                "EstadoCreditos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public EstadoCreditos Insertar(EstadoCreditos entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<EstadoCreditos>(
                "EstadoCreditos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(EstadoCreditos entidad)
        {
            return string.IsNullOrEmpty(entidad.estado);
        }
    }
}