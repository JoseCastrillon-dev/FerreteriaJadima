using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public class CreditosPresentacion : ICreditosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public CreditosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Creditos Actualizar(Creditos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Creditos>(
                "Creditos/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Creditos Borrar(Creditos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Creditos>(
                "Creditos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Creditos> Buscar(Creditos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Creditos>>(
                "Creditos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Creditos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Creditos>>(
                "Creditos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Creditos Insertar(Creditos entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Creditos>(
                "Creditos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Creditos entidad)
        {
            return entidad.idVenta == null ||
                   entidad.id_estadoCredito == null ||
                   entidad.fechaPrestamo == null ||
                   entidad.valorPrestamo == null;
        }
    }
}