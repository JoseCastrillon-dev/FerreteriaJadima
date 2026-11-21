using LibreriaAplicacionFerreteria.nucleo;
using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Front_Ferreteria.Pages.Ventanas
{
    public class EmpleadosModel : PageModel
    {
        private IEmpleadosPresentacion IEmpleadosPresentacion;
        private IPersonasPresentacion IPersonasPresentacion;
        private ISalariosPresentacion ISalariosPresentacion;


        public EmpleadosModel(IEmpleadosPresentacion IEmpleadosPresentacion,
                            IPersonasPresentacion IPersonasPresentacion,
                            ISalariosPresentacion ISalariosPresentacion)
        {
            this.IEmpleadosPresentacion = IEmpleadosPresentacion;
            this.IPersonasPresentacion = IPersonasPresentacion;
            this.ISalariosPresentacion = ISalariosPresentacion;

            this.Filtro = new Empleados();
            Filtro._persona = new Personas();
            Filtro._salario = new Salarios();
        }

        [BindProperty] public List<Empleados> Lista { get; set; }
        [BindProperty] public Empleados? Filtro { get; set; }
        [BindProperty] public Empleados? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        [BindProperty] public List<Personas> ListaPersonas { get; set; }
        [BindProperty] public List<Salarios> listaSalarios { get; set; }





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

                if (this.Filtro!._salario == null)
                {
                    this.Filtro._salario = new Salarios();
                }


                this.Filtro!.carnet = this.Filtro!.carnet ?? "";
                Lista = this.IEmpleadosPresentacion.Buscar(this.Filtro!);
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
                this.Actual!._salario = null;


                if (this.Actual!.id == 0)
                    this.Actual = this.IEmpleadosPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IEmpleadosPresentacion.Actualizar(this.Actual!);
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
                this.Actual = new Empleados();
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

                if (this.Actual!._salario == null)
                {
                    this.Actual._salario = new Salarios();
                }


                if (this.Actual!._persona == null)
                {
                    this.Actual._persona = new Personas ();
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
                this.Actual = this.IEmpleadosPresentacion.Borrar(this.Actual!);
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
            var FiltroPersonas = new Personas { nombre = "" };
            this.ListaPersonas = this.IPersonasPresentacion.Buscar(FiltroPersonas);


            var FiltroSalarios = new Salarios 
            {
                _cargo = new Cargos { nombre = "" },
                _tiposDePago = new TiposDePagos { metodo = ""}
            };
            this.listaSalarios = this.ISalariosPresentacion.Buscar(FiltroSalarios);
        }

        


    }
}
