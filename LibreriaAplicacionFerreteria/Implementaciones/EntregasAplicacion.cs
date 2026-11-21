using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class EntregasAplicacion : IEntregasAplicacion
    {
        private IConexion IConexion;

        public EntregasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Entregas Actualizar(Entregas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");


            var entry = this.IConexion!.Entry<Entregas>(entidad);
            entry.State = EntityState.Modified;
            var Auditoria = new Auditoria
            {
                Desc = "Se Actualizo ",
                fecha = DateTime.Now,
                tablaAfectada = "Entregas"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Entregas Borrar(Entregas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Entregas!.Remove(entidad!);
            var Auditoria = new Auditoria
            {
                Desc = "Se Borro ",
                fecha = DateTime.Now,
                tablaAfectada = "Entregas"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Entregas> Buscar(Entregas entidad)
        {
            var Auditoria = new Auditoria
            {
                Desc = "Se Busco ",
                fecha = DateTime.Now,
                tablaAfectada = "Entregas"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return this.IConexion.Entregas!
                .Include(x => x._domicilio)
                .Include(z => z._empleado)
                .Where(x => x._domicilio!.direccion.Contains(entidad._domicilio.direccion))
                .ToList();
        }

        public List<Entregas> Consultar()
        {
            var Auditoria = new Auditoria
            {
                Desc = "Se Consulto",
                fecha = DateTime.Now,
                tablaAfectada = "Entregas"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return this.IConexion.Entregas!.ToList();
        }

        public Entregas Insertar(Entregas entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            this.IConexion.Entregas!.Add(entidad!);
            var Auditoria = new Auditoria
            {
                Desc = "Se Inserto ",
                fecha = DateTime.Now,
                tablaAfectada = "Entregas"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }


    }
}