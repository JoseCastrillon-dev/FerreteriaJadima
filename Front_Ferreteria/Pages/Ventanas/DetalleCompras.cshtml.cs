using LibreriaAplicacionFerreteria.nucleo;
using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Front_Ferreteria.Pages.Ventanas
{
    public class DetalleComprasModel : PageModel
    {
        private IDetalleComprasPresentacion IDetalleComprasPresentacion;
        private IComprasPresentacion IComprasPresentacion;
        private IProductosPresentacion IProductosPresentacion;


        public DetalleComprasModel(IDetalleComprasPresentacion IDetalleComprasPresentacion,
                                    IComprasPresentacion IComprasPresentacion,
                                    IProductosPresentacion IProductosPresentacion)
        {
            this.IDetalleComprasPresentacion = IDetalleComprasPresentacion;
            this.IComprasPresentacion = IComprasPresentacion;
            this.IProductosPresentacion = IProductosPresentacion;

            this.Filtro = new DetalleCompras();
            Filtro._compra = new Compras();
            Filtro._producto = new Productos();
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public List<DetalleCompras> Lista { get; set; }
        [BindProperty] public DetalleCompras? Filtro { get; set; }
        [BindProperty] public DetalleCompras? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        [BindProperty] public List<Compras> ListaCompras { get; set; }
        [BindProperty] public List<Productos> listaProductos { get; set; }





        public void OnGet()
        {
            OnPostBtBuscar();
        }

        public void OnPostBtBuscar()
        {
            try
            {

                if (this.Filtro!._compra == null)
                {
                    this.Filtro._compra = new Compras();
                }

                if (this.Filtro!._compra._empleado == null)
                {
                    this.Filtro._compra._empleado = new Empleados();
                }

                this.Filtro!._compra._empleado.carnet = this.Filtro!._compra._empleado.carnet ?? "";
                Lista = this.IDetalleComprasPresentacion.Buscar(this.Filtro!);
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
                    this.Actual = this.IDetalleComprasPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IDetalleComprasPresentacion.Actualizar(this.Actual!);
                    OnPostBtBuscar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                CargarDetalleCompras();
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
                this.Actual = new DetalleCompras();
                CargarDetalleCompras();
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


                if (this.Actual!._compra == null)
                {
                    this.Actual._compra = new Compras ();
                }

                Formulario = true;
                OnPostBtBuscar();
                CargarDetalleCompras();
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
                this.Actual = this.IDetalleComprasPresentacion.Borrar(this.Actual!);
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

        private void CargarDetalleCompras()
        {
            var FiltroCompras = new Compras {

                _empleado = new Empleados() { carnet = "" }
            };
            this.ListaCompras = this.IComprasPresentacion.Buscar(FiltroCompras);


            var FiltroProductos = new Productos { codigo = "" };
            this.listaProductos = this.IProductosPresentacion.Buscar(FiltroProductos);
        }

        


    }
}
