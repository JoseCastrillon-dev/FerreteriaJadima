using LibreriaFerreteria.Entidades;
using LibreriaFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VehiculosController : ControllerBase
    {
        private IVehiculosAplicacion IVehiculosAplicacion;

        public VehiculosController(IVehiculosAplicacion IVehiculosAplicacion)
        {
            this.IVehiculosAplicacion = IVehiculosAplicacion;
        }

        [HttpPost]
        public List<Vehiculos> Consultar()
        {
            return this.IVehiculosAplicacion.Consultar();
        }

        [HttpPost]
        public List<Vehiculos> Buscar(Vehiculos entidad)
        {
            return this.IVehiculosAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Vehiculos Insertar(Vehiculos entidad)
        {
            return this.IVehiculosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Vehiculos Actualizar(Vehiculos entidad)
        {
            return this.IVehiculosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Vehiculos Borrar(Vehiculos entidad)
        {
            return this.IVehiculosAplicacion.Borrar(entidad);
        }
    }
}
