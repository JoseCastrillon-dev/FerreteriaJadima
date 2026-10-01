using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TestsFerreteria
{
    [TestClass]
    public class CuotasPruebas : PruebaBase
    {
        private Cuotas? entidad = null;

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
            var credito = CrearCredito();
            var tipoPago = CrearTipoDePago();

            this.entidad = new Cuotas()
            {
                id_credito = credito.id,
                id_tipoDePago = tipoPago.id,
                fechaDePago = DateTime.Now,
                valorDeCuota = 100000m,
                numeroDeCuota = 1
            };
            this.conexion.Cuotas!.Add(this.entidad!);
            this.conexion.SaveChanges();
            Registrar(this.entidad);
        }

        public void Consultar()
        {
            var lista = this.conexion.Cuotas!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.numeroDeCuota = 2;
            var entry = this.conexion!.Entry<Cuotas>(this.entidad!);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Cuotas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
