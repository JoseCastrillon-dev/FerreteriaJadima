using LibreriaAplicacionFerreteria.nucleo;
using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Ferreteria.Pages.Ventanas
{
    public class ProductosModel : PageModel
    {
        private IProductosPresentacion IProductosPresentacion;
        private ITiposPresentacion ITiposPresentacion;

        public ProductosModel(IProductosPresentacion iProductosPresentacion, ITiposPresentacion iTiposPresentacion)
        {
            this.IProductosPresentacion = iProductosPresentacion;
            this.ITiposPresentacion = iTiposPresentacion;
            this.Filtro = new Productos();
            this.Filtro._tipo = new Tipos();
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public List<Productos> Lista { get; set; }
        [BindProperty] public Productos? Filtro { get; set; }
        [BindProperty] public Productos? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        [BindProperty] public List<Tipos> ListaTipos { get; set; }



        public void OnGet()
        {
            OnPostBtBuscar();
        }

        public void OnPostBtBuscar()
        {
            try
            {

                if (this.Filtro!._tipo == null)
                {
                    this.Filtro._tipo = new Tipos();
                }


                this.Filtro!.codigo = this.Filtro!.codigo ?? "";
                Lista = this.IProductosPresentacion.Buscar(this.Filtro!);
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

                this.Actual!._tipo = null;

                if (this.Actual!.id == 0)
                    this.Actual = this.IProductosPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IProductosPresentacion.Actualizar(this.Actual!);
                OnPostBtBuscar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                cargarTipos();
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
                this.Actual = new Productos();
                cargarTipos();

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

                if (this.Actual!._tipo == null)
                {
                    this.Actual._tipo = new Tipos();
                }
                Formulario = true;
                OnPostBtBuscar();
                cargarTipos();

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
                this.Actual = this.IProductosPresentacion.Borrar(this.Actual!);
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

        private void cargarTipos()
        {
            var filtroTipos = new Tipos { nombre = "" };
            this.ListaTipos = this.ITiposPresentacion.Buscar(filtroTipos);
        }

    }
}
