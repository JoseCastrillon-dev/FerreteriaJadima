using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;

namespace LibreriaPresentacionFerreteria.Interfaces
{
    public class CuotasPresentacion : ICuotasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public CuotasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Cuotas Actualizar(Cuotas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Cuotas>(
                "Cuotas/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Cuotas Borrar(Cuotas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Cuotas>(
                "Cuotas/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Cuotas> Buscar(Cuotas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Cuotas>>(
                "Cuotas/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Cuotas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Cuotas>>(
                "Cuotas/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Cuotas Insertar(Cuotas entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Cuotas>(
                "Cuotas/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Cuotas entidad)
        {
            return entidad.id_credito == null ||
                   entidad.id_tipoDePago == null ||
                   entidad.fechaDePago == null ||
                   entidad.valorDeCuota == null ||
                   entidad.numeroDeCuota == null; 
        }
    }
}