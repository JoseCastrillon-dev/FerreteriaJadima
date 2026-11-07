using LibreriaAplicacionFerreteria.Entidades;
using LibreriaAplicacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DetalleComprasController : ControllerBase
    {
        private IDetalleComprasAplicacion IDetalleComprasAplicacion;

        public DetalleComprasController(IDetalleComprasAplicacion IDetalleComprasAplicacion)
        {
            this.IDetalleComprasAplicacion = IDetalleComprasAplicacion;
        }

        [HttpPost]
        public List<DetalleCompras> Consultar()
        {
            return this.IDetalleComprasAplicacion.Consultar();
        }

        [HttpPost]
        public List<DetalleCompras> Buscar(DetalleCompras entidad)
        {
            return this.IDetalleComprasAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public DetalleCompras Insertar(DetalleCompras entidad)
        {
            return this.IDetalleComprasAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public DetalleCompras Actualizar(DetalleCompras entidad)
        {
            return this.IDetalleComprasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public DetalleCompras Borrar(DetalleCompras entidad)
        {
            return this.IDetalleComprasAplicacion.Borrar(entidad);
        }
    }
}
