using LibreriaAplicacionFerreteria.Entidades;
using LibreriaAplicacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DomiciliosController : ControllerBase
    {
        private IDomiciliosAplicacion IDomiciliosAplicacion;

        public DomiciliosController(IDomiciliosAplicacion IDomiciliosAplicacion)
        {
            this.IDomiciliosAplicacion = IDomiciliosAplicacion;
        }

        [HttpPost]
        public List<Domicilios> Consultar()
        {
            return this.IDomiciliosAplicacion.Consultar();
        }

        [HttpPost]
        public List<Domicilios> Buscar(Domicilios entidad)
        {
            return this.IDomiciliosAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Domicilios Insertar(Domicilios entidad)
        {
            return this.IDomiciliosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Domicilios Actualizar(Domicilios entidad)
        {
            return this.IDomiciliosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Domicilios Borrar(Domicilios entidad)
        {
            return this.IDomiciliosAplicacion.Borrar(entidad);
        }
    }
}
