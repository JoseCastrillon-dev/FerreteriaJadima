using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public class SalariosPresentacion : ISalariosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public SalariosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Salarios Actualizar(Salarios entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Salarios>(
                "Salarios/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Salarios Borrar(Salarios entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Salarios>(
                "Salarios/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Salarios> Buscar(Salarios entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Salarios>>(
                "Salarios/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Salarios> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Salarios>>(
                "Salarios/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Salarios Insertar(Salarios entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Salarios>(
                "Salarios/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Salarios entidad)
        {
            return entidad.id_cargo == null ||
                   entidad.id_tiposDePago == null ||
                   entidad.valorHora == null ||
                   entidad.horasTrabajadas == null;
        }
    }
}