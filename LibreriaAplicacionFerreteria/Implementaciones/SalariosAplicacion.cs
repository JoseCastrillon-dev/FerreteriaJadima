using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class SalariosAplicacion : ISalariosAplicacion
    {
        private IConexion IConexion;

        public SalariosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Salarios Actualizar(Salarios entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            var entry = this.IConexion!.Entry<Salarios>(entidad);
            entry.State = EntityState.Modified;
            var Auditoria = new Auditoria
            {
                Desc = "Se Actualizo ",
                fecha = DateTime.Now,
                tablaAfectada = "Salarios"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Salarios Borrar(Salarios entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Salarios!.Remove(entidad!);
            var Auditoria = new Auditoria
            {
                Desc = "Se Borro ",
                fecha = DateTime.Now,
                tablaAfectada = "Salarios"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Salarios> Buscar(Salarios entidad)
        {
            var Auditoria = new Auditoria
            {
                Desc = "Se Busco ",
                fecha = DateTime.Now,
                tablaAfectada = "Salarios"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return this.IConexion.Salarios!
                .Include(x => x._cargo)
                .Include(x => x._tiposDePago)
                .Where(x => x._cargo!.nombre!.Contains(entidad._cargo.nombre))
                .Where(x => x._tiposDePago!.metodo!.Contains(entidad._tiposDePago.metodo))
                .Take(10)
                .ToList();
        }

        public List<Salarios> Consultar()
        {
            var Auditoria = new Auditoria
            {
                Desc = "Se Consulto ",
                fecha = DateTime.Now,
                tablaAfectada = "Salarios"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return this.IConexion.Salarios!.ToList();
        }

        public Salarios Insertar(Salarios entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");


            this.IConexion.Salarios!.Add(entidad!);
            var Auditoria = new Auditoria
            {
                Desc = "Se Inserto ",
                fecha = DateTime.Now,
                tablaAfectada = "Salarios"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }

    }
}