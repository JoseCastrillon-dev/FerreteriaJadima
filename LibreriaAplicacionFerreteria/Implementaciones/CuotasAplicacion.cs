using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class CuotasAplicacion : ICuotasAplicacion
    {
        private IConexion IConexion;

        public CuotasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Cuotas Actualizar(Cuotas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");


            var entry = this.IConexion!.Entry<Cuotas>(entidad);
            entry.State = EntityState.Modified;
            var Auditoria = new Auditoria
            {
                Desc = "Se Actualizo ",
                fecha = DateTime.Now,
                tablaAfectada = "Cuotas"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Cuotas Borrar(Cuotas entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Cuotas!.Remove(entidad!);
            var Auditoria = new Auditoria
            {
                Desc = "Se Borro ",
                fecha = DateTime.Now,
                tablaAfectada = "Cuotas"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Cuotas> Buscar(Cuotas entidad)
        {
            var Auditoria = new Auditoria
            {
                Desc = "Se Busco ",
                fecha = DateTime.Now,
                tablaAfectada = "Cuotas"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return this.IConexion.Cuotas!
                .Include(x => x._credito)
                .Include(y => y._tipoDePago)
                .Include(x => x._credito!._estadoCredito)
                .Where(x => x._credito!._estadoCredito!.estado
                .Contains(entidad._credito._estadoCredito.estado))
                .Take(10)
                .ToList();
        }

        public List<Cuotas> Consultar()
        {
            var Auditoria = new Auditoria
            {
                Desc = "Se Consulto ",
                fecha = DateTime.Now,
                tablaAfectada = "Cuotas"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return this.IConexion.Cuotas!.ToList();
        }

        public Cuotas Insertar(Cuotas entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            this.IConexion.Cuotas!.Add(entidad!);
            var Auditoria = new Auditoria
            {
                Desc = "Se Inserto ",
                fecha = DateTime.Now,
                tablaAfectada = "Cuotas"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }


    }
}