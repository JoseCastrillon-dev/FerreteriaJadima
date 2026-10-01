using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class Personas
    {
        public int id { get; set; }
        public String? cedula { get; set; }
        public String? nombre { get; set; }
        public String? apellido { get; set; }
        public String? numeroTelefono { get; set; }

        public List<Clientes>? Clientes { get; set; }
        public List<Empleados>? Empleados { get; set; }
    }
}
