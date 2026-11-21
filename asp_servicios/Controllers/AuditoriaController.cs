using LibreriaAplicacionFerreteria.Entidades;
using LibreriaAplicacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuditoriaController : ControllerBase
    {
        private IAuditoriaAplicacion IAuditoriaAplicacion;

        public AuditoriaController(IAuditoriaAplicacion IAuditoriaAplicacion)
        {
            this.IAuditoriaAplicacion = IAuditoriaAplicacion;
        }

        [HttpPost]
        public List<Auditoria> Consultar()
        {
            return this.IAuditoriaAplicacion.Consultar();
        }

        [HttpPost]
        public List<Auditoria> Buscar(Auditoria entidad)
        {
            return this.IAuditoriaAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Auditoria Insertar(Auditoria entidad)
        {
            return this.IAuditoriaAplicacion.Insertar(entidad);
        }
    }
}
