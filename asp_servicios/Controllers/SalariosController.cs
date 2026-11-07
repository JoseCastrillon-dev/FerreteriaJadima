using LibreriaAplicacionFerreteria.Entidades;
using LibreriaAplicacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SalariosController : ControllerBase
    {
        private ISalariosAplicacion ISalariosAplicacion;

        public SalariosController(ISalariosAplicacion ISalariosAplicacion)
        {
            this.ISalariosAplicacion = ISalariosAplicacion;
        }

        [HttpPost]
        public List<Salarios> Consultar()
        {
            return this.ISalariosAplicacion.Consultar();
        }

        [HttpPost]
        public List<Salarios> Buscar(Salarios entidad)
        {
            return this.ISalariosAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Salarios Insertar(Salarios entidad)
        {
            return this.ISalariosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Salarios Actualizar(Salarios entidad)
        {
            return this.ISalariosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Salarios Borrar(Salarios entidad)
        {
            return this.ISalariosAplicacion.Borrar(entidad);
        }
    }
}
