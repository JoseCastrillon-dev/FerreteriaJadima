using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TestsFerreteria
{
    [TestClass]
    public class EntregasPruebas : PruebaBase
    {
        private Entregas? entidad = null;

        [TestMethod]
        public void Execute()
        {
            try
            {
                Insertar();
                Consultar();
                Actualizar();
                Borrar();
            }
            finally
            {
                Limpiar();
            }
        }

        public void Insertar()
        {
            var domicilio = CrearDomicilio();
            var empleado = CrearEmpleado();

            this.entidad = new Entregas()
            {
                id_domicilio = domicilio.id,
                id_empleado = empleado.id,
                fechaSalida = DateTime.Now,
                fechaLlegada = DateTime.Now.AddHours(1)
            };
            this.conexion.Entregas!.Add(this.entidad!);
            this.conexion.SaveChanges();
            Registrar(this.entidad);
        }

        public void Consultar()
        {
            var lista = this.conexion.Entregas!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.fechaLlegada = DateTime.Now.AddHours(2);
            var entry = this.conexion!.Entry<Entregas>(this.entidad!);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Entregas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
