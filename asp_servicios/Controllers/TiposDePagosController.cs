using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TiposDePagosController : ControllerBase
    {
        private ITiposDePagosAplicacion ITiposDePagosAplicacion;

        public TiposDePagosController(ITiposDePagosAplicacion ITiposDePagosAplicacion)
        {
            this.ITiposDePagosAplicacion = ITiposDePagosAplicacion;
        }

        [HttpPost]
        public List<TiposDePagos> Consultar()
        {
            return this.ITiposDePagosAplicacion.Consultar();
        }

        [HttpPost]
        public List<TiposDePagos> Buscar(TiposDePagos entidad)
        {
            return this.ITiposDePagosAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public TiposDePagos Insertar(TiposDePagos entidad)
        {
            return this.ITiposDePagosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public TiposDePagos Actualizar(TiposDePagos entidad)
        {
            return this.ITiposDePagosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public TiposDePagos Borrar(TiposDePagos entidad)
        {
            return this.ITiposDePagosAplicacion.Borrar(entidad);
        }
    }
}
