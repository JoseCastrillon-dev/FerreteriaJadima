using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TestsFerreteria
{
    [TestClass]
    public class ProductosPruebas : PruebaBase
    {
        private Productos? entidad = null;

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
            var tipo = CrearTipo();

            this.entidad = new Productos()
            {
                id_tipo = tipo.id,
                codigo = "P-" + Sufijo(8),
                cantidad = 100,
                nombre = "Martillo-" + Sufijo(8),
                valor = 10000m
            };
            this.conexion.Productos!.Add(this.entidad!);
            this.conexion.SaveChanges();
            Registrar(this.entidad);
        }

        public void Consultar()
        {
            var lista = this.conexion.Productos!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.valor = 12000m;
            var entry = this.conexion!.Entry<Productos>(this.entidad!);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Productos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
