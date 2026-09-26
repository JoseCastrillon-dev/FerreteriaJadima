using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class Entregas
    {
        public int id { get; set; }
        public int id_domicilio { get; set; }
        public int id_empleado { get; set; }
        public DateTime? fechaSalida { get; set; }
        public DateTime? fechaLlegada { get; set; }

        [ForeignKey("id_domicilio")] public Domicilios? _domicilio { get; set; }
        [ForeignKey("id_empleado")] public Empleados? _empleado { get; set; }
    }
}

