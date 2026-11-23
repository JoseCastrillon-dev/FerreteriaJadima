using LibreriaAplicacionFerreteria.nucleo;
using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Front_Ferreteria.Pages.Ventanas
{
    public class ComprasModel : PageModel
    {
        private IComprasPresentacion IComprasPresentacion;
        private IProveedoresPresentacion IProveedoresPresentacion;
        private IEmpleadosPresentacion IEmpleadosPresentacion;


        public ComprasModel(IComprasPresentacion IComprasPresentacion,
                            IProveedoresPresentacion IProveedoresPresentacion,
                            IEmpleadosPresentacion IEmpleadosPresentacion)
        {
            this.IComprasPresentacion = IComprasPresentacion;
            this.IProveedoresPresentacion = IProveedoresPresentacion;
            this.IEmpleadosPresentacion = IEmpleadosPresentacion;

            this.Filtro = new Compras();
            Filtro._proveedor = new Proveedores();
            Filtro._empleado = new Empleados();
        }

        public IFormFile? FormFile { get; set; }
        [BindProperty] public List<Compras> Lista { get; set; }
        [BindProperty] public Compras? Filtro { get; set; }
        [BindProperty] public Compras? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        [BindProperty] public List<Proveedores> ListaProveedores { get; set; }
        [BindProperty] public List<Empleados> listaEmpleados { get; set; }





        public void OnGet()
        {
            OnPostBtBuscar();
        }

        public void OnPostBtBuscar()
        {
            try
            {

                if (this.Filtro!._proveedor == null)
                {
                    this.Filtro._proveedor = new Proveedores();
                }

                if (this.Filtro!._empleado == null)
                {
                    this.Filtro._empleado = new Empleados();
                }


                this.Filtro!._empleado.carnet = this.Filtro!._empleado.carnet ?? "";
                Lista = this.IComprasPresentacion.Buscar(this.Filtro!);
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
                    this.Actual = this.IComprasPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IComprasPresentacion.Actualizar(this.Actual!);
                    OnPostBtBuscar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                CargarProveedores();
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
                this.Actual = new Compras();
                CargarProveedores();
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

                if (this.Actual!._empleado == null)
                {
                    this.Actual._empleado = new Empleados();
                }


                if (this.Actual!._proveedor == null)
                {
                    this.Actual._proveedor = new Proveedores ();
                }

                Formulario = true;
                OnPostBtBuscar();
                CargarProveedores();
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
                this.Actual = this.IComprasPresentacion.Borrar(this.Actual!);
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

        private void CargarProveedores()
        {
            var FiltroProveedores = new Proveedores { nombre = "" };
            this.ListaProveedores = this.IProveedoresPresentacion.Buscar(FiltroProveedores);


            var FiltroEmpleados = new Empleados { carnet = "" };
            this.listaEmpleados = this.IEmpleadosPresentacion.Buscar(FiltroEmpleados);
        }

        


    }
}
