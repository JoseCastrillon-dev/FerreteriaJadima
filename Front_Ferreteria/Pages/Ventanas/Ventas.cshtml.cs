using LibreriaAplicacionFerreteria.nucleo;
using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Front_Ferreteria.Pages.Ventanas
{
    public class VentasModel : PageModel
    {
        private IVentasPresentacion IVentasPresentacion;
        private IDetalleVentasPresentacion IDetalleVentasPresentacion;
        private ITiposDePagosPresentacion ITiposDePagosPresentacion;
        private IEmpleadosPresentacion IEmpleadosPresentacion;
        private IClientesPresentacion IClientesPresentacion;


        public VentasModel(IVentasPresentacion IVentasPresentacion,
                           IDetalleVentasPresentacion IDetalleVentasPresentacion,
                           ITiposDePagosPresentacion ITiposDePagosPresentacion,
                           IEmpleadosPresentacion iEmpleadosPresentacion,
                           IClientesPresentacion iClientesPresentacion)
        {
            this.IVentasPresentacion = IVentasPresentacion;
            this.IDetalleVentasPresentacion = IDetalleVentasPresentacion;
            this.ITiposDePagosPresentacion = ITiposDePagosPresentacion;
            this.IEmpleadosPresentacion = iEmpleadosPresentacion;
            this.IClientesPresentacion = iClientesPresentacion;

            this.Filtro = new Ventas();
            Filtro._detalleVenta = new DetalleVentas();
            Filtro._tipoDePago = new TiposDePagos();
            Filtro._empleado = new Empleados();
            Filtro._cliente = new Clientes();
        }

        [BindProperty] public List<Ventas> Lista { get; set; }
        [BindProperty] public Ventas? Filtro { get; set; }
        [BindProperty] public Ventas? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        [BindProperty] public List<DetalleVentas> ListaDetalleVentas { get; set; }
        [BindProperty] public List<TiposDePagos> ListaTiposDePagos { get; set; }
        [BindProperty] public List<Empleados> ListaEmpleados { get; set; }
        [BindProperty] public List<Clientes> ListaClientes { get; set; }





        public void OnGet()
        {
            OnPostBtBuscar();
        }

        public void OnPostBtBuscar()
        {
            try
            {

                if (this.Filtro!._detalleVenta == null)
                {
                    this.Filtro._detalleVenta = new DetalleVentas();
                }
                if (this.Filtro!._detalleVenta._producto == null)
                {
                    this.Filtro!._detalleVenta._producto = new Productos();
                }


                this.Filtro!._detalleVenta._producto.codigo = this.Filtro!._detalleVenta._producto.codigo ?? "";
                Lista = this.IVentasPresentacion.Buscar(this.Filtro!);
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
                    this.Actual = this.IVentasPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IVentasPresentacion.Actualizar(this.Actual!);
                    OnPostBtBuscar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                CargarVentas();
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
                this.Actual = new Ventas();
                CargarVentas();
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

                if (this.Actual!._detalleVenta == null)
                {
                    this.Actual._detalleVenta = new DetalleVentas();
                }


                if (this.Actual!._tipoDePago == null)
                {
                    this.Actual._tipoDePago = new TiposDePagos();
                }

                if (this.Actual!._empleado == null)
                {
                    this.Actual._empleado = new Empleados();
                }

                if (this.Actual!._cliente== null)
                {
                    this.Actual._cliente = new Clientes();
                }

                Formulario = true;
                OnPostBtBuscar();
                CargarVentas();
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
                this.Actual = this.IVentasPresentacion.Borrar(this.Actual!);
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

        private void CargarVentas()
        {
            var FiltroDetalleVentas = new DetalleVentas {

                _producto = new Productos() { codigo = "" }
            };
            this.ListaDetalleVentas = this.IDetalleVentasPresentacion.Buscar(FiltroDetalleVentas);


            var FiltroTiposDePago = new TiposDePagos { metodo = "" };
            this.ListaTiposDePagos = this.ITiposDePagosPresentacion.Buscar(FiltroTiposDePago);

            var FiltroEmpleado = new Empleados { carnet = "" };
            this.ListaEmpleados = this.IEmpleadosPresentacion.Buscar(FiltroEmpleado);

            var FiltroCliente = new Clientes { 
                
                _persona = new Personas { nombre = "" } };
            this.ListaClientes = this.IClientesPresentacion.Buscar(FiltroCliente);
        }

        


    }
}
