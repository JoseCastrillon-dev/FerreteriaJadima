using LibreriaAplicacionFerreteria.nucleo;
using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Front_Ferreteria.Pages.Ventanas
{
    public class EstanteriasModel : PageModel
    {
        private IEstanteriasPresentacion IEstanteriasPresentacion;
        private IEmpleadosPresentacion IEmpleadosPresentacion;
        private IProductosPresentacion IProductosPresentacion;


        public EstanteriasModel(IEstanteriasPresentacion IEstanteriasPresentacion,
                            IEmpleadosPresentacion IEmpleadosPresentacion,
                            IProductosPresentacion IProductosPresentacion)
        {
            this.IEstanteriasPresentacion = IEstanteriasPresentacion;
            this.IEmpleadosPresentacion = IEmpleadosPresentacion;
            this.IProductosPresentacion = IProductosPresentacion;

            this.Filtro = new Estanterias();
            Filtro._empleado = new Empleados();
            Filtro._producto = new Productos();
        }

        [BindProperty] public List<Estanterias> Lista { get; set; }
        [BindProperty] public Estanterias? Filtro { get; set; }
        [BindProperty] public Estanterias? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        [BindProperty] public List<Empleados> ListaEmpleados { get; set; }
        [BindProperty] public List<Productos> listaProductos { get; set; }





        public void OnGet()
        {
            OnPostBtBuscar();
        }

        public void OnPostBtBuscar()
        {
            try
            {

                if (this.Filtro!._empleado == null)
                {
                    this.Filtro._empleado = new Empleados();
                }

                if (this.Filtro!._producto == null)
                {
                    this.Filtro._producto = new Productos();
                }


                this.Filtro!.codigo = this.Filtro!.codigo ?? "";
                Lista = this.IEstanteriasPresentacion.Buscar(this.Filtro!);
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

                this.Actual!._empleado = null;
                this.Actual!._producto = null;


                if (this.Actual!.id == 0)
                    this.Actual = this.IEstanteriasPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IEstanteriasPresentacion.Actualizar(this.Actual!);
                    OnPostBtBuscar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                CargarEmpleados();
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
                this.Actual = new Estanterias();
                CargarEmpleados();
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

                if (this.Actual!._producto == null)
                {
                    this.Actual._producto = new Productos();
                }


                if (this.Actual!._empleado == null)
                {
                    this.Actual._empleado = new Empleados ();
                }

                Formulario = true;
                OnPostBtBuscar();
                CargarEmpleados();
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
                this.Actual = this.IEstanteriasPresentacion.Borrar(this.Actual!);
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

        private void CargarEmpleados()
        {
            var FiltroEmpleados = new Empleados { carnet = "" };
            this.ListaEmpleados = this.IEmpleadosPresentacion.Buscar(FiltroEmpleados);


            var FiltroProductos = new Productos { codigo = "" };
            this.listaProductos = this.IProductosPresentacion.Buscar(FiltroProductos);
        }

        


    }
}
