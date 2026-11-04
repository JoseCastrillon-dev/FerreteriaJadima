using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VentasController : ControllerBase
    {
        private IVentasAplicacion IVentasAplicacion;

        public VentasController(IVentasAplicacion IVentasAplicacion)
        {
            this.IVentasAplicacion = IVentasAplicacion;
        }

        [HttpPost]
        public List<Ventas> Consultar()
        {
            return this.IVentasAplicacion.Consultar();
        }

        [HttpPost]
        public List<Ventas> Buscar(Ventas entidad)
        {
            return this.IVentasAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Ventas Insertar(Ventas entidad)
        {
            return this.IVentasAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Ventas Actualizar(Ventas entidad)
        {
            return this.IVentasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Ventas Borrar(Ventas entidad)
        {
            return this.IVentasAplicacion.Borrar(entidad);
        }
    }
}
