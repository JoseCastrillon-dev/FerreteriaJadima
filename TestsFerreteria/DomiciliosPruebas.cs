using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TestsFerreteria
{
    [TestClass]
    public class DomiciliosPruebas : PruebaBase
    {
        private Domicilios? entidad = null;

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
            var vehiculo = CrearVehiculo();

            this.entidad = new Domicilios()
            {
                id_venta = venta.id,
                id_vehiculo = vehiculo.id,
                numeroDeTelefono = "3001234567",
                direccion = "Calle 10 # 20-30"
            };
            this.conexion.Domicilios!.Add(this.entidad!);
            this.conexion.SaveChanges();
            Registrar(this.entidad);
        }

        public void Consultar()
        {
            var lista = this.conexion.Domicilios!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.direccion = "Carrera 45 # 12-08";
            var entry = this.conexion!.Entry<Domicilios>(this.entidad!);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Domicilios!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
