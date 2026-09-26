using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class Estanterias
    {
        public int id {  get; set; }
        public int id_empleado { get; set; }
        public int id_producto { get; set; }
        public String? codigo { get; set; }
        public int Stock { get; set; }

        [ForeignKey("id_empleado")] public Empleados? _empleado { get; set; }
        [ForeignKey("id_producto")] public Productos? _producto { get; set; }

    }
}
