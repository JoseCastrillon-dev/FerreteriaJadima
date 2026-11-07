using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public class DetalleComprasPresentacion : IDetalleComprasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public DetalleComprasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public DetalleCompras Actualizar(DetalleCompras entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<DetalleCompras>(
                "DetalleCompras/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public DetalleCompras Borrar(DetalleCompras entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<DetalleCompras>(
                "DetalleCompras/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<DetalleCompras> Buscar(DetalleCompras entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<DetalleCompras>>(
                "DetalleCompras/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<DetalleCompras> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<DetalleCompras>>(
                "DetalleCompras/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public DetalleCompras Insertar(DetalleCompras entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<DetalleCompras>(
                "DetalleCompras/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(DetalleCompras entidad)
        {
            return entidad.id_compra == null ||
                   entidad.id_producto == null ||
                   entidad.cantidad == null ||
                   entidad.preciosCompra == null;
        }
    }
}