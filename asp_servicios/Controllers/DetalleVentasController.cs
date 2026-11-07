using LibreriaAplicacionFerreteria.Entidades;
using LibreriaAplicacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DetalleVentasController : ControllerBase
    {
        private IDetalleVentasAplicacion IDetalleVentasAplicacion;

        public DetalleVentasController(IDetalleVentasAplicacion IDetalleVentasAplicacion)
        {
            this.IDetalleVentasAplicacion = IDetalleVentasAplicacion;
        }

        [HttpPost]
        public List<DetalleVentas> Consultar()
        {
            return this.IDetalleVentasAplicacion.Consultar();
        }

        [HttpPost]
        public List<DetalleVentas> Buscar(DetalleVentas entidad)
        {
            return this.IDetalleVentasAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public DetalleVentas Insertar(DetalleVentas entidad)
        {
            return this.IDetalleVentasAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public DetalleVentas Actualizar(DetalleVentas entidad)
        {
            return this.IDetalleVentasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public DetalleVentas Borrar(DetalleVentas entidad)
        {
            return this.IDetalleVentasAplicacion.Borrar(entidad);
        }
    }
}
