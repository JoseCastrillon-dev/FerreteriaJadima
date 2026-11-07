using LibreriaAplicacionFerreteria.Entidades;
using LibreriaAplicacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EstanteriasController : ControllerBase
    {
        private IEstanteriasAplicacion IEstanteriasAplicacion;

        public EstanteriasController(IEstanteriasAplicacion IEstanteriasAplicacion)
        {
            this.IEstanteriasAplicacion = IEstanteriasAplicacion;
        }

        [HttpPost]
        public List<Estanterias> Consultar()
        {
            return this.IEstanteriasAplicacion.Consultar();
        }

        [HttpPost]
        public List<Estanterias> Buscar(Estanterias entidad)
        {
            return this.IEstanteriasAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Estanterias Insertar(Estanterias entidad)
        {
            return this.IEstanteriasAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Estanterias Actualizar(Estanterias entidad)
        {
            return this.IEstanteriasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Estanterias Borrar(Estanterias entidad)
        {
            return this.IEstanteriasAplicacion.Borrar(entidad);
        }
    }
}
