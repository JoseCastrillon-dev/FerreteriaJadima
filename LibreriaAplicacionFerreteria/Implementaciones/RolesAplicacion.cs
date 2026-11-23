using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class RolesAplicacion : IRolesAplicacion
    {
        private IConexion IConexion;

        public RolesAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public List<Roles> Consultar()
        {
            return this.IConexion.Roles!.ToList();
        }
        public Roles Borrar(Roles entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Roles!.Remove(entidad!);
            var Auditoria = new Auditoria
            {
                Desc = "Se Borro ",
                fecha = DateTime.Now,
                tablaAfectada = "Roles"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }
        
        public Roles Insertar(Roles entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Roles!.Add(entidad!); 
            var Auditoria = new Auditoria
            {
                Desc = "Se inserto ",
                fecha = DateTime.Now,
                tablaAfectada = "Roles"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }
        public List<Roles> Buscar(Roles entidad)
        {
            var text = entidad.nombre ?? string.Empty;

            return this.IConexion.Roles!
                .Where(x => x.nombre!.Contains(text))
                            .ToList();
        }

        private bool Validar(Roles entidad)
        {
            return
                string.IsNullOrEmpty(entidad.nombre);
        }
    }
}