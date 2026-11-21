using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class ComprasAplicacion : IComprasAplicacion
    {
        private IConexion IConexion;

        public ComprasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Compras Actualizar(Compras entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");


            var entry = this.IConexion!.Entry<Compras>(entidad);
            entry.State = EntityState.Modified;
            var Auditoria = new Auditoria
            {
                Desc = "Se Actualizo ",
                fecha = DateTime.Now,
                tablaAfectada = "Compras"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Compras Borrar(Compras entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Compras!.Remove(entidad!); 
            var Auditoria = new Auditoria
            {
                Desc = "Se Borro ",
                fecha = DateTime.Now,
                tablaAfectada = "Compras"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Compras> Buscar(Compras entidad)
        {
            var Auditoria = new Auditoria
            {
                Desc = "Se Busco ",
                fecha = DateTime.Now,
                tablaAfectada = "Compras"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return this.IConexion.Compras!
                .Include(x => x._empleado).Include(x => x._proveedor)
                .Where(x => x._empleado!.carnet!.Contains(entidad._empleado!.carnet!)).Take(10).ToList();
                
        }

        public List<Compras> Consultar()
        {
            var Auditoria = new Auditoria
            {
                Desc = "Se Consulto ",
                fecha = DateTime.Now,
                tablaAfectada = "Compras"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return this.IConexion.Compras!.ToList();
        }

        public Compras Insertar(Compras entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            this.IConexion.Compras!.Add(entidad!); 
            var Auditoria = new Auditoria
            {
                Desc = "Se Inserto ",
                fecha = DateTime.Now,
                tablaAfectada = "Compras"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }

    }
}