using LibreriaAplicacionFerreteria.nucleo;
using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Ferreteria.Pages.Ventanas
{
    public class ClientesModel : PageModel
    {
        private IClientesPresentacion IClientesPresentacion;
        private IPersonasPresentacion IPersonasPresentacion;

        public ClientesModel(IClientesPresentacion IClientesPresentacion, IPersonasPresentacion IPersonasPresentacion)
        {
            this.IClientesPresentacion = IClientesPresentacion;
            this.IPersonasPresentacion = IPersonasPresentacion;
            this.Filtro = new Clientes();
            Filtro._persona = new Personas();
        }

        [BindProperty] public List<Clientes> Lista { get; set; }
        [BindProperty] public Clientes? Filtro { get; set; }
        [BindProperty] public Clientes? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        [BindProperty] public List<Personas> ListaPersonas { get; set; }




        public void OnGet()
        {
            OnPostBtBuscar();
        }

        public void OnPostBtBuscar()
        {
            try
            {

                if (this.Filtro!._persona == null)
                {
                    this.Filtro._persona = new Personas();
                }

                this.Filtro!._persona!.nombre = this.Filtro!._persona.nombre ?? "";
                Lista = this.IClientesPresentacion.Buscar(this.Filtro!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {

                this.Actual!._persona = null;

                if (this.Actual!.id == 0)
                    this.Actual = this.IClientesPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IClientesPresentacion.Actualizar(this.Actual!);
                    OnPostBtBuscar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                CargarPersonas();
            }
            finally
            {
                OnPostBtBuscar();
            }
        }

        public void OnPostBtNuevo()
        {
            try
            {
                Formulario = true;
                this.Actual = new Clientes();
                CargarPersonas();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtEditar(string data)
        {
            try
            {

                if (this.Actual!._persona == null)
                {
                    this.Actual._persona = new Personas();
                }

                Formulario = true;
                OnPostBtBuscar();
                CargarPersonas();
                Actual = this.Lista.FirstOrDefault(x => x.id == Convert.ToInt32(data));
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtPregBorrar(string data)
        {
            try
            {
                Borrar = true;
                OnPostBtBuscar();
                Actual = this.Lista.FirstOrDefault(x => x.id == Convert.ToInt32(data));
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (this.Actual!.id == 0)
                    return;
                this.Actual = this.IClientesPresentacion.Borrar(this.Actual!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
            finally
            {
                OnPostBtBuscar();
            }
        }

        private void CargarPersonas()
        {
            var filtroPersona = new Personas { nombre = "" };
            this.ListaPersonas = this.IPersonasPresentacion.Buscar(filtroPersona);
        }


    }
}
