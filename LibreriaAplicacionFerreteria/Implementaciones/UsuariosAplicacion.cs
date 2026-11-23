using LibreriaAplicacionFerreteria.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaAplicacionFerreteria.Interfaces
{
    public class UsuariosAplicacion : IUsuariosAplicacion
    {
        private IConexion IConexion;

        public UsuariosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public List<Usuarios> Consultar()
        {
            return this.IConexion.Usuarios!.ToList();
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
            this.IConexion.SaveChanges();
            return entidad;
        }
        public List<Usuarios> Buscar(Usuarios entidad)
        {
            var text = entidad.Usuario ?? string.Empty;

            return this.IConexion.Usuarios!
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