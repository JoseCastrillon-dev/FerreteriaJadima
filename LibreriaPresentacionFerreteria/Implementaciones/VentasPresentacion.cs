using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public class VentasPresentacion : IVentasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public VentasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Ventas Actualizar(Ventas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Ventas>(
                "Ventas/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Ventas Borrar(Ventas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Ventas>(
                "Ventas/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Ventas> Buscar(Ventas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Ventas>>(
                "Ventas/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Ventas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Ventas>>(
                "Ventas/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Ventas Insertar(Ventas entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Ventas>(
                "Ventas/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Ventas entidad)
        {
            return entidad.id_detalleVenta == null ||
                   entidad.id_tipoDePago == null ||
                   entidad.id_empleado == null ||
                   entidad.id_cliente == null ||
                   entidad.iva == null ||
                   entidad.total == null;
        }
    }
}