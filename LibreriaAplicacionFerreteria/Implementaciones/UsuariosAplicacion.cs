using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class UsuariosAplicacion : IUsuariosAplicacion
    {
        private IConexion IConexion;

        public UsuariosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }
        public Usuarios Actualizar(Usuarios entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var entry = this.IConexion!.Entry<Usuarios>(entidad);
            entry.State = EntityState.Modified;
            var Auditoria = new Auditoria
            {
                Desc = "Se Actualizo ",
                fecha = DateTime.Now,
                tablaAfectada = "Usuarios"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion!.SaveChanges();
            return entidad;
        }
        public List<Usuarios> Consultar()
        {
            return this.IConexion.Usuarios!
                .ToList();
        }
        public Usuarios Borrar(Usuarios entidad)
        {
            if (entidad.id == 0)
                throw new Exception("No se ha guardo");

            this.IConexion.Usuarios!.Remove(entidad!);
            var Auditoria = new Auditoria
            {
                Desc = "Se Borro ",
                fecha = DateTime.Now,
                tablaAfectada = "Usuarios"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }
        public Usuarios Ingresar(Usuarios entidad)
        {
            if (string.IsNullOrEmpty(entidad.Usuario) || string.IsNullOrEmpty(entidad.Contraseña))
                return new Usuarios();               

            var encontrado = this.IConexion.Usuarios!
                .FirstOrDefault(x => x.Usuario == entidad.Usuario
                                  && x.Contraseña == entidad.Contraseña);

            if (encontrado == null)
                return new Usuarios();

            var Auditoria = new Auditoria
            {
                Desc = "Ingreso el usuario " + encontrado.Usuario,
                fecha = DateTime.Now,
                tablaAfectada = "inicio de Sesion"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion!.SaveChanges();
            
            var permiso = this.IConexion.Permisos!.FirstOrDefault(p => p.id_rol == encontrado.rol);

            encontrado.Ver = permiso?.ver ?? false;
            encontrado.Guardar = permiso?.guardar ?? false;
            encontrado.Borrar = permiso?.borrar ?? false;

            encontrado.Contraseña = null;
            return encontrado;
        }

        public Usuarios Insertar(Usuarios entidad)
        {
            if (entidad.id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            this.IConexion.Usuarios!.Add(entidad!); 
            var Auditoria = new Auditoria
            {
                Desc = "Se inserto ",
                fecha = DateTime.Now,
                tablaAfectada = "Usuarios"
            };
            this.IConexion.Auditoria!.Add(Auditoria!);
            this.IConexion.SaveChanges();
            return entidad;
        }
        public List<Usuarios> Buscar(Usuarios entidad)
        {
            var text = entidad.Usuario ?? string.Empty;

            return this.IConexion.Usuarios!
                .Include(x => x._roles)
                .Where(x => x.Usuario!.Contains(text))
                .ToList();
        }

        private bool Validar(Usuarios entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Usuario);
        }
    }
}