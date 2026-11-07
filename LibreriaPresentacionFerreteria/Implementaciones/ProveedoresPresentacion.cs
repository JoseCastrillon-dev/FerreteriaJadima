using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public class ProveedoresPresentacion : IProveedoresPresentacion
    {
        private IComunicaciones IComunicaciones;

        public ProveedoresPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Proveedores Actualizar(Proveedores entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Proveedores>(
                "Proveedores/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Proveedores Borrar(Proveedores entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Proveedores>(
                "Proveedores/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Proveedores> Buscar(Proveedores entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Proveedores>>(
                "Proveedores/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Proveedores> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Proveedores>>(
                "Proveedores/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Proveedores Insertar(Proveedores entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Proveedores>(
                "Proveedores/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Proveedores entidad)
        {
            return string.IsNullOrEmpty(entidad.nombre) ||
                   string.IsNullOrEmpty(entidad.numeroDeTelefono) ||
                   string.IsNullOrEmpty(entidad.direccion) ||
                    string.IsNullOrEmpty(entidad.email);
        }
    }
}