using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TestsFerreteria
{
    [TestClass]
    public class EstanteriasPruebas : PruebaBase
    {
        private Estanterias? entidad = null;

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
            var empleado = CrearEmpleado();
            var producto = CrearProducto();

            this.entidad = new Estanterias()
            {
                id_empleado = empleado.id,
                id_producto = producto.id,
                codigo = "EST-" + Sufijo(8),
                Stock = 50
            };
            this.conexion.Estanterias!.Add(this.entidad!);
            this.conexion.SaveChanges();
            Registrar(this.entidad);
        }

        public void Consultar()
        {
            var lista = this.conexion.Estanterias!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Stock = 40;
            var entry = this.conexion!.Entry<Estanterias>(this.entidad!);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Estanterias!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
