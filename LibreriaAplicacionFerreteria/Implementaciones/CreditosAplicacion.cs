using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class CreditosAplicacion : ICreditosAplicacion
    {
        private IConexion IConexion;

        public CreditosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Creditos Actualizar(Creditos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var entry = this.IConexion!.Entry<Creditos>(entidad);
            entry.State = EntityState.Modified;
            var Auditoria = new Auditoria
            {
                Desc = "Se Actualizo ",
                fecha = DateTime.Now,
                tablaAfectada = "Creditos"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Creditos Borrar(Creditos entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Creditos!.Remove(entidad!);
            var Auditoria = new Auditoria
            {
                Desc = "Se Borro ",
                fecha = DateTime.Now,
                tablaAfectada = "Creditos"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Creditos> Buscar(Creditos entidad)
        {
            var Auditoria = new Auditoria
            {
                Desc = "Se Busco ",
                fecha = DateTime.Now,
                tablaAfectada = "Creditos"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return this.IConexion.Creditos!
                .Include(x => x._venta)
                .Include(x => x._estadoCredito)
                .Include(x => x._venta!._detalleVenta)
                .Include(x => x._venta!._cliente)
                .Include(x => x._venta!._cliente!._persona)
                .Where(x => x._venta!._cliente!._persona!.nombre.Contains(entidad._venta._cliente._persona.nombre))
                .Take(10)
                .ToList();
        }

        public List<Creditos> Consultar()
        {
            var Auditoria = new Auditoria
            {
                Desc = "Se Consulto ",
                fecha = DateTime.Now,
                tablaAfectada = "Creditos"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();

            return this.IConexion.Creditos!.ToList();
        }

        public Creditos Insertar(Creditos entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            this.IConexion.Creditos!.Add(entidad!);
            var Auditoria = new Auditoria
            {
                Desc = "Se Inserto ",
                fecha = DateTime.Now,
                tablaAfectada = "Creditos"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }


    }
}