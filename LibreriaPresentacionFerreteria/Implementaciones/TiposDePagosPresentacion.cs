using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public class TiposDePagosPresentacion : ITiposDePagosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public TiposDePagosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public TiposDePagos Actualizar(TiposDePagos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<TiposDePagos>(
                "TiposDePagos/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public TiposDePagos Borrar(TiposDePagos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<TiposDePagos>(
                "TiposDePagos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<TiposDePagos> Buscar(TiposDePagos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<TiposDePagos>>(
                "TiposDePagos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<TiposDePagos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<TiposDePagos>>(
                "TiposDePagos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public TiposDePagos Insertar(TiposDePagos entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<TiposDePagos>(
                "TiposDePagos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(TiposDePagos entidad)
        {
            return string.IsNullOrEmpty(entidad.metodo);
        }
    }
}