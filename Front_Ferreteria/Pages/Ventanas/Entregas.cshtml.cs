using LibreriaAplicacionFerreteria.nucleo;
using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Front_Ferreteria.Pages.Ventanas
{
    public class EntregasModel : PageModel
    {
        private IEntregasPresentacion IEntregasPresentacion;
        private IDomiciliosPresentacion IDomiciliosPresentacion;
        private IEmpleadosPresentacion IEmpleadosPresentacion;


        public EntregasModel(IEntregasPresentacion IEntregasPresentacion,
                                    IDomiciliosPresentacion IDomiciliosPresentacion,
                                    IEmpleadosPresentacion IEmpleadosPresentacion)
        {
            this.IEntregasPresentacion = IEntregasPresentacion;
            this.IDomiciliosPresentacion = IDomiciliosPresentacion;
            this.IEmpleadosPresentacion = IEmpleadosPresentacion;

            this.Filtro = new Entregas();
            Filtro._domicilio = new Domicilios();
            Filtro._empleado = new Empleados();
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public List<Entregas> Lista { get; set; }
        [BindProperty] public Entregas? Filtro { get; set; }
        [BindProperty] public Entregas? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        [BindProperty] public List<Domicilios> ListaDomicilios { get; set; }
        [BindProperty] public List<Empleados> listaEmpleados { get; set; }





        public void OnGet()
        {
            OnPostBtBuscar();
        }

        public void OnPostBtBuscar()
        {
            try
            {

                if (this.Filtro!._domicilio == null)
                {
                    this.Filtro._domicilio = new Domicilios();
                }
   

                this.Filtro!._domicilio.direccion = this.Filtro!._domicilio.direccion ?? "";
                Lista = this.IEntregasPresentacion.Buscar(this.Filtro!);
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
                if (FormFile != null)
                {
                    var memoryStream = new MemoryStream();
                    FormFile.CopyToAsync(memoryStream).Wait();
                    Actual!.imagen = Convert.ToBase64String(memoryStream.ToArray());
                    memoryStream.Dispose();
                }
                if (this.Actual!.id == 0)
                    this.Actual = this.IEntregasPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IEntregasPresentacion.Actualizar(this.Actual!);
                    OnPostBtBuscar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                CargarEntregas();
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
                this.Actual = new Entregas();
                CargarEntregas();
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

                if (this.Actual!._domicilio == null)
                {
                    this.Actual._domicilio = new Domicilios();
                }


                if (this.Actual!._empleado == null)
                {
                    this.Actual._empleado = new Empleados ();
                }

                Formulario = true;
                OnPostBtBuscar();
                CargarEntregas();
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
                this.Actual = this.IEntregasPresentacion.Borrar(this.Actual!);
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

        private void CargarEntregas()
        {
            var FiltroDomicilios = new Domicilios { direccion = "" };
            this.ListaDomicilios = this.IDomiciliosPresentacion.Buscar(FiltroDomicilios);


            var FiltroEmpleado = new Empleados { carnet = "" };
            this.listaEmpleados = this.IEmpleadosPresentacion.Buscar(FiltroEmpleado);
        }

        


    }
}
