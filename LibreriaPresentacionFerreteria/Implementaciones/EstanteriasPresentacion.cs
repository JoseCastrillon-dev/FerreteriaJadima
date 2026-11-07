using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public class EstanteriasPresentacion : IEstanteriasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public EstanteriasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Estanterias Actualizar(Estanterias entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Estanterias>(
                "Estanterias/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Estanterias Borrar(Estanterias entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Estanterias>(
                "Estanterias/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Estanterias> Buscar(Estanterias entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Estanterias>>(
                "Estanterias/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Estanterias> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Estanterias>>(
                "Estanterias/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Estanterias Insertar(Estanterias entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Estanterias>(
                "Estanterias/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Estanterias entidad)
        {
            return entidad.id_empleado == null ||
                   entidad.id_producto == null ||
                   string.IsNullOrEmpty(entidad.codigo) ||
                   entidad.Stock == null;
        }
    }
}