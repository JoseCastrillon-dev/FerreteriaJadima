using LibreriaAplicacionFerreteria.nucleo;
using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Ferreteria.Pages.Ventanas
{
    public class DetalleVentasModel : PageModel
    {
        private IDetalleVentasPresentacion IDetalleVentasPresentacion;
        private IProductosPresentacion IProductosPresentacion;

        public DetalleVentasModel(IDetalleVentasPresentacion iDetalleVentasPresentacion, IProductosPresentacion IProductosPresentacion)
        {
            this.IDetalleVentasPresentacion = iDetalleVentasPresentacion;
            this.IProductosPresentacion = IProductosPresentacion;

            this.Filtro = new DetalleVentas();
            this.Filtro!._producto = new Productos();
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public List<DetalleVentas> Lista { get; set; }
        [BindProperty] public DetalleVentas? Filtro { get; set; }
        [BindProperty] public DetalleVentas? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        [BindProperty] public List<Productos> ListaProductos { get; set; }



        public void OnGet()
        {
            OnPostBtBuscar();
        }

        public void OnPostBtBuscar()
        {
            try
            {

                if (this.Filtro!._producto == null)
                {
                    this.Filtro._producto = new Productos();
                }


                this.Filtro!._producto.codigo = this.Filtro!._producto.codigo ?? "";
                Lista = this.IDetalleVentasPresentacion.Buscar(this.Filtro!);
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
                this.Actual!._producto = null;

                if (this.Actual!.id == 0)
                    this.Actual = this.IDetalleVentasPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IDetalleVentasPresentacion.Actualizar(this.Actual!);
                OnPostBtBuscar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                cargarProductos();
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
                this.Actual = new DetalleVentas();
                cargarProductos();

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
                Formulario = true;
                OnPostBtBuscar();
                cargarProductos();

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
                this.Actual = this.IDetalleVentasPresentacion.Borrar(this.Actual!);
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

        private void cargarProductos()
        {
            var filtroProductos = new Productos { codigo = "" };
            this.ListaProductos = this.IProductosPresentacion.Buscar(filtroProductos);
        }

    }
}
