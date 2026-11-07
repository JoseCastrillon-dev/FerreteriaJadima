using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public class EntregasPresentacion : IEntregasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public EntregasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Entregas Actualizar(Entregas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Entregas>(
                "Entregas/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Entregas Borrar(Entregas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Entregas>(
                "Entregas/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Entregas> Buscar(Entregas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Entregas>>(
                "Entregas/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Entregas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Entregas>>(
                "Entregas/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Entregas Insertar(Entregas entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Entregas>(
                "Entregas/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Entregas entidad)
        {
            return entidad.id_domicilio == null ||
                   entidad.id_empleado == null ||
                   entidad.fechaSalida == null ||
                   entidad.fechaLlegada == null;
        }
    }
}