using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TestsFerreteria
{
    [TestClass]
    public class VentasPruebas : PruebaBase
    {
        private Ventas? entidad = null;

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
            var detalle = CrearDetalleVenta();
            var tipoPago = CrearTipoDePago();
            var empleado = CrearEmpleado();
            var cliente = CrearCliente();

            this.entidad = new Ventas()
            {
                id_detalleVenta = detalle.id,
                id_tipoDePago = tipoPago.id,
                id_empleado = empleado.id,
                id_cliente = cliente.id,
                iva = 3800m,
                total = 23800m
            };
            this.conexion.Ventas!.Add(this.entidad!);
            this.conexion.SaveChanges();
            Registrar(this.entidad);
        }

        public void Consultar()
        {
            var lista = this.conexion.Ventas!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.total = 25000m;
            var entry = this.conexion!.Entry<Ventas>(this.entidad!);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Ventas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
