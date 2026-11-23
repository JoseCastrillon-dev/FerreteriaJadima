using LibreriaAplicacionFerreteria.Entidades;
using LibreriaAplicacionFerreteria.nucleo;
using LibreriaPresentacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Ferreteria.Pages
{
    public class IndexModel : PageModel
    {
        private IUsuariosPresentacion IUsuariosPresentacion;

        public IndexModel(IUsuariosPresentacion IUsuariosPresentacion)
        {
            this.IUsuariosPresentacion = IUsuariosPresentacion;
        }
        public bool EstaLogueado = false;
        [BindProperty] public string? Usuario { get; set; }
        [BindProperty] public string? Contraseña { get; set; }
        [BindProperty] public string? Mensaje { get; set; }

        public void OnGet()
        {
            var variable_session = HttpContext.Session.GetString("Usuario");
            if (!String.IsNullOrEmpty(variable_session))
            {
                EstaLogueado = true;
                return;
            }
        }


        public void OnPostBtClean()
        {
            try
            {
                Usuario = string.Empty;
                Contraseña = string.Empty;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtEnter()
        {
            try
            {
                var usuario = this.IUsuariosPresentacion.Ingresar(
                    new Usuarios { Usuario = Usuario, Contraseña = Contraseña });

                if (usuario.id == 0)
                {
                    OnPostBtClean();
                    Mensaje = "Usuario o contraseña incorrectos";
                    return;
                }

                HttpContext.Session.SetString("Usuario", usuario.Usuario!);
                HttpContext.Session.SetString("P_ver", usuario.Ver ? "1" : "0");
                HttpContext.Session.SetString("P_guardar", usuario.Guardar ? "1" : "0");
                HttpContext.Session.SetString("P_borrar", usuario.Borrar ? "1" : "0");
                EstaLogueado = true;
                OnPostBtClean();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtClose()
        {
            try
            {
                HttpContext.Session.Clear();
                HttpContext.Response.Redirect("/");
                EstaLogueado = false;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}
