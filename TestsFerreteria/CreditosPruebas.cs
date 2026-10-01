using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TestsFerreteria
{
    [TestClass]
    public class CreditosPruebas : PruebaBase
    {
        private Creditos? entidad = null;

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
            var venta = CrearVenta();
            var estado = CrearEstadoCredito();

            this.entidad = new Creditos()
            {
                idVenta = venta.id,
                id_estadoCredito = estado.id,
                fechaPrestamo = DateTime.Now,
                valorPrestamo = 500000m
            };
            this.conexion.Creditos!.Add(this.entidad!);
            this.conexion.SaveChanges();
            Registrar(this.entidad);
        }

        public void Consultar()
        {
            var lista = this.conexion.Creditos!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.valorPrestamo = 600000m;
            var entry = this.conexion!.Entry<Creditos>(this.entidad!);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Creditos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
