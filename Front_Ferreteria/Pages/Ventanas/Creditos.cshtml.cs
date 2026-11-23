using LibreriaAplicacionFerreteria.nucleo;
using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Front_Ferreteria.Pages.Ventanas
{
    public class CreditosModel : PageModel
    {
        private ICreditosPresentacion ICreditosPresentacion;
        private IVentasPresentacion IVentasPresentacion;
        private IEstadoCreditosPresentacion IEstadoCreditosPresentacion;


        public CreditosModel(ICreditosPresentacion ICreditosPresentacion,
                                    IVentasPresentacion IVentasPresentacion,
                                    IEstadoCreditosPresentacion IEstadoCreditosPresentacion)
        {
            this.ICreditosPresentacion = ICreditosPresentacion;
            this.IVentasPresentacion = IVentasPresentacion;
            this.IEstadoCreditosPresentacion = IEstadoCreditosPresentacion;

            this.Filtro = new Creditos();
            Filtro._venta = new Ventas();
            Filtro._estadoCredito = new EstadoCreditos();
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public List<Creditos> Lista { get; set; }
        [BindProperty] public Creditos? Filtro { get; set; }
        [BindProperty] public Creditos? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        [BindProperty] public List<Ventas> ListaVentas { get; set; }
        [BindProperty] public List<EstadoCreditos> listaEstadoCreditos { get; set; }





        public void OnGet()
        {
            OnPostBtBuscar();
        }

        public void OnPostBtBuscar()
        {
            try
            {

                if (this.Filtro!._venta == null)
                {
                    this.Filtro._venta = new Ventas();
                }

                if (this.Filtro!._venta!._cliente == null)
                {
                    this.Filtro._venta!._cliente = new Clientes();
                }

                if (this.Filtro!._venta!._cliente!._persona == null)
                {
                    this.Filtro!._venta!._cliente!._persona = new Personas();
                }
                if (this.Filtro!._venta!._detalleVenta == null)
                {
                    this.Filtro!._venta!._detalleVenta = new DetalleVentas();
                }


                this.Filtro!._venta._cliente._persona.nombre = this.Filtro!._venta._cliente._persona.nombre ?? "";
                Lista = this.ICreditosPresentacion.Buscar(this.Filtro!);
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
                    this.Actual = this.ICreditosPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.ICreditosPresentacion.Actualizar(this.Actual!);
                    OnPostBtBuscar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                CargarCreditos();
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
                this.Actual = new Creditos();
                CargarCreditos();
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

                if (this.Actual!._estadoCredito == null)
                {
                    this.Actual._estadoCredito = new EstadoCreditos();
                }


                if (this.Actual!._venta == null)
                {
                    this.Actual._venta = new Ventas ();
                }

                Formulario = true;
                OnPostBtBuscar();
                CargarCreditos();
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
                this.Actual = this.ICreditosPresentacion.Borrar(this.Actual!);
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

        private void CargarCreditos()
        {
            var FiltroVentas = new Ventas {

                _empleado = new Empleados() { carnet = "" }
            };
            this.ListaVentas = this.IVentasPresentacion.Buscar(FiltroVentas);


            var FiltroEstadoCreditos = new EstadoCreditos { estado = "" };
            this.listaEstadoCreditos = this.IEstadoCreditosPresentacion.Buscar(FiltroEstadoCreditos);
        }

        


    }
}
