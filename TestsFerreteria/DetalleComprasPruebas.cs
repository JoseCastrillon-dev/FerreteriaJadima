using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TestsFerreteria
{
    [TestClass]
    public class DetalleComprasPruebas : PruebaBase
    {
        private DetalleCompras? entidad = null;

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
            var compra = CrearCompra();
            var producto = CrearProducto();

            this.entidad = new DetalleCompras()
            {
                id_compra = compra.id,
                id_producto = producto.id,
                cantidad = 5,
                preciosCompra = 12000m
            };
            this.conexion.DetalleCompras!.Add(this.entidad!);
            this.conexion.SaveChanges();
            Registrar(this.entidad);
        }

        public void Consultar()
        {
            var lista = this.conexion.DetalleCompras!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.cantidad = 8;
            var entry = this.conexion!.Entry<DetalleCompras>(this.entidad!);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.DetalleCompras!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
