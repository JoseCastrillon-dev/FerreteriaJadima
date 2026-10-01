using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TestsFerreteria
{
    [TestClass]
    public class VehiculosPruebas : PruebaBase
    {
        private Vehiculos? entidad = null;

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
            this.entidad = new Vehiculos()
            {
                matricula = Sufijo(8),
                marca = "Chevrolet",
                numeroDeChasis = "CH" + Sufijo(20),
                numeroMotor = "MT" + Sufijo(20)
            };
            this.conexion.Vehiculos!.Add(this.entidad!);
            this.conexion.SaveChanges();
            Registrar(this.entidad);
        }

        public void Consultar()
        {
            var lista = this.conexion.Vehiculos!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.marca = "Renault";
            var entry = this.conexion!.Entry<Vehiculos>(this.entidad!);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Vehiculos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
