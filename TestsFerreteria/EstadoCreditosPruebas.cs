using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TestsFerreteria
{
    [TestClass]
    public class EstadoCreditosPruebas : PruebaBase
    {
        private EstadoCreditos? entidad = null;

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
            this.entidad = new EstadoCreditos()
            {
                estado = "Vigente"
            };
            this.conexion.EstadoCreditos!.Add(this.entidad!);
            this.conexion.SaveChanges();
            Registrar(this.entidad);
        }

        public void Consultar()
        {
            var lista = this.conexion.EstadoCreditos!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.estado = "Pagado";
            var entry = this.conexion!.Entry<EstadoCreditos>(this.entidad!);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.EstadoCreditos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
