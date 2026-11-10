using LibreriaAplicacionFerreteria.nucleo;
using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Ferreteria.Pages.Ventanas
{
    public class EstadoCreditosModel : PageModel
    {
        private IEstadoCreditosPresentacion IEstadoCreditosPresentacion;

        public EstadoCreditosModel(IEstadoCreditosPresentacion iEstadoCreditosPresentacion)
        {
            this.IEstadoCreditosPresentacion = iEstadoCreditosPresentacion;
            this.Filtro = new EstadoCreditos();
        }

        [BindProperty] public List<EstadoCreditos> Lista { get; set; }
        [BindProperty] public EstadoCreditos? Filtro { get; set; }
        [BindProperty] public EstadoCreditos? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;

        public void OnGet()
        {
            OnPostBtBuscar();
        }

        public void OnPostBtBuscar()
        {
            try
            {
                this.Filtro!.estado = this.Filtro!.estado ?? "";
                Lista = this.IEstadoCreditosPresentacion.Buscar(this.Filtro!);
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
                    this.Actual = this.IEstadoCreditosPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IEstadoCreditosPresentacion.Actualizar(this.Actual!);
                OnPostBtBuscar();
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

        public void OnPostBtNuevo()
        {
            try
            {
                Formulario = true;
                this.Actual = new EstadoCreditos();
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
                Formulario = true;
                OnPostBtBuscar();
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
                this.Actual = this.IEstadoCreditosPresentacion.Borrar(this.Actual!);
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


    }
}
