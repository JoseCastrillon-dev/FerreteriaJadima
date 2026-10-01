using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TestsFerreteria
{
    [TestClass]
    public class DetalleVentasPruebas : PruebaBase
    {
        private DetalleVentas? entidad = null;

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
            var producto = CrearProducto();

            this.entidad = new DetalleVentas()
            {
                id_producto = producto.id,
                cantidad = 2,
                iva = 3800m,
                total = 23800m
            };
            this.conexion.DetalleVentas!.Add(this.entidad!);
            this.conexion.SaveChanges();
            Registrar(this.entidad);
        }

        public void Consultar()
        {
            var lista = this.conexion.DetalleVentas!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.cantidad = 3;
            var entry = this.conexion!.Entry<DetalleVentas>(this.entidad!);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.DetalleVentas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
