using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TiposController : ControllerBase
    {
        private ITiposAplicacion ITiposAplicacion;

        public TiposController(ITiposAplicacion ITiposAplicacion)
        {
            this.ITiposAplicacion = ITiposAplicacion;
        }

        [HttpPost]
        public List<Tipos> Consultar()
        {
            return this.ITiposAplicacion.Consultar();
        }

        [HttpPost]
        public List<Tipos> Buscar(Tipos entidad)
        {
            return this.ITiposAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Tipos Insertar(Tipos entidad)
        {
            return this.ITiposAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Tipos Actualizar(Tipos entidad)
        {
            return this.ITiposAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Tipos Borrar(Tipos entidad)
        {
            return this.ITiposAplicacion.Borrar(entidad);
        }
    }
}
