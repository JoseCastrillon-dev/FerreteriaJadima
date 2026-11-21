using LibreriaAplicacionFerreteria.nucleo;
using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Front_Ferreteria.Pages.Ventanas
{
    public class DomiciliosModel : PageModel
    {
        private IDomiciliosPresentacion IDomiciliosPresentacion;
        private IVentasPresentacion IVentasPresentacion;
        private IVehiculosPresentacion IVehiculosPresentacion;


        public DomiciliosModel(IDomiciliosPresentacion IDomiciliosPresentacion,
                                    IVentasPresentacion IVentasPresentacion,
                                    IVehiculosPresentacion IVehiculosPresentacion)
        {
            this.IDomiciliosPresentacion = IDomiciliosPresentacion;
            this.IVentasPresentacion = IVentasPresentacion;
            this.IVehiculosPresentacion = IVehiculosPresentacion;

            this.Filtro = new Domicilios();
            Filtro._venta = new Ventas();
            Filtro._vehiculos = new Vehiculos();
        }

        [BindProperty] public List<Domicilios> Lista { get; set; }
        [BindProperty] public Domicilios? Filtro { get; set; }
        [BindProperty] public Domicilios? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        [BindProperty] public List<Ventas> ListaVentas { get; set; }
        [BindProperty] public List<Vehiculos> listaVehiculos { get; set; }





        public void OnGet()
        {
            OnPostBtBuscar();
        }

        public void OnPostBtBuscar()
        {
            try
            {


                this.Filtro!.direccion = this.Filtro!.direccion ?? "";
                Lista = this.IDomiciliosPresentacion.Buscar(this.Filtro!);
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

                if (this.Actual!.id == 0)
                    this.Actual = this.IDomiciliosPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IDomiciliosPresentacion.Actualizar(this.Actual!);
                    OnPostBtBuscar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                CargarDomicilios();
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
                this.Actual = new Domicilios();
                CargarDomicilios();
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

                if (this.Actual!._vehiculos == null)
                {
                    this.Actual._vehiculos = new Vehiculos();
                }


                if (this.Actual!._venta == null)
                {
                    this.Actual._venta = new Ventas ();
                }

                Formulario = true;
                OnPostBtBuscar();
                CargarDomicilios();
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
                this.Actual = this.IDomiciliosPresentacion.Borrar(this.Actual!);
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

        private void CargarDomicilios()
        {
            var FiltroVentas = new Ventas
            {

                _detalleVenta = new DetalleVentas {
                    _producto = new Productos { codigo = ""} }

            };
            this.ListaVentas = this.IVentasPresentacion.Buscar(FiltroVentas);


            var FiltroVehiculos = new Vehiculos { matricula = "" };
            this.listaVehiculos = this.IVehiculosPresentacion.Buscar(FiltroVehiculos);
        }

        


    }
}
