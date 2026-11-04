using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CuotasController : ControllerBase
    {
        private ICuotasAplicacion ICuotasAplicacion;

        public CuotasController(ICuotasAplicacion ICuotasAplicacion)
        {
            this.ICuotasAplicacion = ICuotasAplicacion;
        }

        [HttpPost]
        public List<Cuotas> Consultar()
        {
            return this.ICuotasAplicacion.Consultar();
        }

        [HttpPost]
        public List<Cuotas> Buscar(Cuotas entidad)
        {
            return this.ICuotasAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Cuotas Insertar(Cuotas entidad)
        {
            return this.ICuotasAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Cuotas Actualizar(Cuotas entidad)
        {
            return this.ICuotasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Cuotas Borrar(Cuotas entidad)
        {
            return this.ICuotasAplicacion.Borrar(entidad);
        }
    }
}
