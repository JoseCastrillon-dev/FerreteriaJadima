using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TestsFerreteria
{
    [TestClass]
    public class SalariosPruebas : PruebaBase
    {
        private Salarios? entidad = null;

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
            var cargo = CrearCargo();
            var tipoPago = CrearTipoDePago();

            this.entidad = new Salarios()
            {
                id_cargo = cargo.id,
                id_tiposDePago = tipoPago.id,
                valorHora = 8000m,
                horasTrabajadas = 160
            };
            this.conexion.Salarios!.Add(this.entidad!);
            this.conexion.SaveChanges();
            Registrar(this.entidad);
        }

        public void Consultar()
        {
            var lista = this.conexion.Salarios!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.horasTrabajadas = 180;
            var entry = this.conexion!.Entry<Salarios>(this.entidad!);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Salarios!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
