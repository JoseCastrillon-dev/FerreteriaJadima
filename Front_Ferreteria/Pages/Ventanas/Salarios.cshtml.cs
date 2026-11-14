using LibreriaAplicacionFerreteria.nucleo;
using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Ferreteria.Pages.Ventanas
{
    public class SalariosModel : PageModel
    {
        private ISalariosPresentacion ISalariosPresentacion;
        private ICargosPresentacion ICargosPresentacion;
        private ITiposDePagosPresentacion ITiposDePagosPresentacion;


        public SalariosModel(ISalariosPresentacion ISalariosPresentacion,
                            ICargosPresentacion ICargosPresentacion,
                            ITiposDePagosPresentacion iTiposDePagosPresentacion)
        {
            this.ISalariosPresentacion = ISalariosPresentacion;
            this.ICargosPresentacion = ICargosPresentacion;
            ITiposDePagosPresentacion = iTiposDePagosPresentacion;

            this.Filtro = new Salarios();
            Filtro._cargo = new Cargos();
            Filtro._tiposDePago = new TiposDePagos();
        }

        [BindProperty] public List<Salarios> Lista { get; set; }
        [BindProperty] public Salarios? Filtro { get; set; }
        [BindProperty] public Salarios? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        [BindProperty] public List<Cargos> ListaCargos { get; set; }
        [BindProperty] public List<TiposDePagos> ListaTiposPagos { get; set; }





        public void OnGet()
        {
            OnPostBtBuscar();
        }

        public void OnPostBtBuscar()
        {
            try
            {

                if (this.Filtro!._cargo == null)
                {
                    this.Filtro._cargo = new Cargos();
                }

                if (this.Filtro!._tiposDePago == null)
                {
                    this.Filtro._tiposDePago = new TiposDePagos();
                }

                this.Filtro!._cargo!.nombre = this.Filtro!._cargo.nombre ?? "";
                this.Filtro!._tiposDePago!.metodo = this.Filtro!._tiposDePago.metodo ?? "";
                Lista = this.ISalariosPresentacion.Buscar(this.Filtro!);
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

                this.Actual!._cargo = null;
                this.Actual!._tiposDePago = null;


                if (this.Actual!.id == 0)
                    this.Actual = this.ISalariosPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.ISalariosPresentacion.Actualizar(this.Actual!);
                    OnPostBtBuscar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                CargarCargos();
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
                this.Actual = new Salarios();
                CargarCargos();
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

                if (this.Actual!._cargo == null)
                {
                    this.Actual._cargo = new Cargos();
                }


                if (this.Actual!._tiposDePago == null)
                {
                    this.Actual._tiposDePago = new TiposDePagos();
                }

                Formulario = true;
                OnPostBtBuscar();
                CargarCargos();
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
                this.Actual = this.ISalariosPresentacion.Borrar(this.Actual!);
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

        private void CargarCargos()
        {
            var FiltroCargos = new Cargos { nombre = "" };
            this.ListaCargos = this.ICargosPresentacion.Buscar(FiltroCargos);

            var FiltroTipoPagos = new TiposDePagos { metodo = "" };
            this.ListaTiposPagos = this.ITiposDePagosPresentacion.Buscar(FiltroTipoPagos);

        }

        


    }
}
