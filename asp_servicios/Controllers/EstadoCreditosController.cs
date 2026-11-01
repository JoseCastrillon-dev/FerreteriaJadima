
using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EstadoCreditosController : ControllerBase
    {
        private IEstadoCreditosAplicacion IEstadoCreditoAplicacion;

        public EstadoCreditosController(IEstadoCreditosAplicacion IEstadoCreditoAplicacion)
        {
            this.IEstadoCreditoAplicacion = IEstadoCreditoAplicacion;
        }

        [HttpPost]
        public List<EstadoCreditos> Consultar()
        {
            return this.IEstadoCreditoAplicacion.Consultar();
        }

        [HttpPost]
        public List<EstadoCreditos> Buscar(EstadoCreditos entidad)
        {
            return this.IEstadoCreditoAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public EstadoCreditos Insertar(EstadoCreditos entidad)
        {
            return this.IEstadoCreditoAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public EstadoCreditos Actualizar(EstadoCreditos entidad)
        {
            return this.IEstadoCreditoAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public EstadoCreditos Borrar(EstadoCreditos entidad)
        {
            return this.IEstadoCreditoAplicacion.Borrar(entidad);
        }
    }
}
