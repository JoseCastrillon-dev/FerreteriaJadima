using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public class DetalleVentasPresentacion : IDetalleVentasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public DetalleVentasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public DetalleVentas Actualizar(DetalleVentas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<DetalleVentas>(
                "DetalleVentas/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public DetalleVentas Borrar(DetalleVentas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<DetalleVentas>(
                "DetalleVentas/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<DetalleVentas> Buscar(DetalleVentas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<DetalleVentas>>(
                "DetalleVentas/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<DetalleVentas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<DetalleVentas>>(
                "DetalleVentas/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public DetalleVentas Insertar(DetalleVentas entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<DetalleVentas>(
                "DetalleVentas/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(DetalleVentas entidad)
        {
            return entidad.id_producto == null ||
                   entidad.cantidad == null ||
                   entidad.iva == null ||
                   entidad.total == null;
        }
    }
}