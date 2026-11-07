using LibreriaAplicacionFerreteria.Entidades;
using LibreriaAplicacionFerreteria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmpleadosController : ControllerBase
    {
        private IEmpleadosAplicacion IEmpleadosAplicacion;

        public EmpleadosController(IEmpleadosAplicacion IEmpleadosAplicacion)
        {
            this.IEmpleadosAplicacion = IEmpleadosAplicacion;
        }

        [HttpPost]
        public List<Empleados> Consultar()
        {
            return this.IEmpleadosAplicacion.Consultar();
        }

        [HttpPost]
        public List<Empleados> Buscar(Empleados entidad)
        {
            return this.IEmpleadosAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Empleados Insertar(Empleados entidad)
        {
            return this.IEmpleadosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Empleados Actualizar(Empleados entidad)
        {
            return this.IEmpleadosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Empleados Borrar(Empleados entidad)
        {
            return this.IEmpleadosAplicacion.Borrar(entidad);
        }
    }
}
