using LibreriaAplicacionFerreteria.nucleo;
using LibreriaAplicacionFerreteria.Entidades;
using LibreriaPresentacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_Ferreteria.Pages.Ventanas
{
    public class AuditoriaModel : PageModel
    {
        private IAuditoriaPresentacion IAuditoriaPresentacion;

        public AuditoriaModel(IAuditoriaPresentacion iAuditoriaPresentacion)
        {
            this.IAuditoriaPresentacion = iAuditoriaPresentacion;
            this.Filtro = new Auditoria();
        }

        [BindProperty] public List<Auditoria> Lista { get; set; }
        [BindProperty] public Auditoria? Filtro { get; set; }
        [BindProperty] public Auditoria? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;

        public void OnGet()
        {
            OnPostBtBuscar();
        }

        public void OnPostBtBuscar()
        {
            try
            {
                this.Filtro!.tablaAfectada = this.Filtro!.tablaAfectada ?? "";
                this.Filtro!.Desc = this.Filtro!.Desc ?? "";
                Lista = this.IAuditoriaPresentacion.Buscar(this.Filtro!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}
