using LibreriaFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TestsFerreteria
{
    [TestClass]
    public class EmpleadosPruebas : PruebaBase
    {
        private Empleados? entidad = null;

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
            var persona = CrearPersona();
            var salario = CrearSalario();

            this.entidad = new Empleados()
            {
                id_persona = persona.id,
                id_salario = salario.id,
                carnet = "E" + Sufijo(10),
                Genero = "Masculino"
            };
            this.conexion.Empleados!.Add(this.entidad!);
            this.conexion.SaveChanges();
            Registrar(this.entidad);
        }

        public void Consultar()
        {
            var lista = this.conexion.Empleados!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.carnet = "E" + Sufijo(10);
            var entry = this.conexion!.Entry<Empleados>(this.entidad!);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Empleados!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
