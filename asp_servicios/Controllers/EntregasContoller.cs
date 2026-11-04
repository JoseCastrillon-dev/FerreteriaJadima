using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EntregasController : ControllerBase
    {
        private IEntregasAplicacion IEntregasAplicacion;

        public EntregasController(IEntregasAplicacion IEntregasAplicacion)
        {
            this.IEntregasAplicacion = IEntregasAplicacion;
        }

        [HttpPost]
        public List<Entregas> Consultar()
        {
            return this.IEntregasAplicacion.Consultar();
        }

        [HttpPost]
        public List<Entregas> Buscar(Entregas entidad)
        {
            return this.IEntregasAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Entregas Insertar(Entregas entidad)
        {
            return this.IEntregasAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Entregas Actualizar(Entregas entidad)
        {
            return this.IEntregasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Entregas Borrar(Entregas entidad)
        {
            return this.IEntregasAplicacion.Borrar(entidad);
        }
    }
}
