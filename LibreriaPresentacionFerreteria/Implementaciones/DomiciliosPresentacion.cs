using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public class DomiciliosPresentacion : IDomiciliosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public DomiciliosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Domicilios Actualizar(Domicilios entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Domicilios>(
                "Domicilios/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Domicilios Borrar(Domicilios entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Domicilios>(
                "Domicilios/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Domicilios> Buscar(Domicilios entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Domicilios>>(
                "Domicilios/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Domicilios> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Domicilios>>(
                "Domicilios/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Domicilios Insertar(Domicilios entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Domicilios>(
                "Domicilios/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Domicilios entidad)
        {
            return entidad.id_venta == null ||
                   entidad.id_vehiculo == null ||
                   entidad.numeroDeTelefono == null ||
                   entidad.direccion == null;
        }
    }
}