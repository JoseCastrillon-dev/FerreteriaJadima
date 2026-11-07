using LibreriaAplicacionFerreteria.Entidades;
using LibreriaAplicacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CreditosController : ControllerBase
    {
        private ICreditosAplicacion ICreditosAplicacion;

        public CreditosController(ICreditosAplicacion ICreditosAplicacion)
        {
            this.ICreditosAplicacion = ICreditosAplicacion;
        }

        [HttpPost]
        public List<Creditos> Consultar()
        {
            return this.ICreditosAplicacion.Consultar();
        }

        [HttpPost]
        public List<Creditos> Buscar(Creditos entidad)
        {
            return this.ICreditosAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Creditos Insertar(Creditos entidad)
        {
            return this.ICreditosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Creditos Actualizar(Creditos entidad)
        {
            return this.ICreditosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Creditos Borrar(Creditos entidad)
        {
            return this.ICreditosAplicacion.Borrar(entidad);
        }
    }
}
