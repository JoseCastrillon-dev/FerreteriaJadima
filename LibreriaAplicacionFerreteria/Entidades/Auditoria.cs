using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaAplicacionFerreteria.Entidades
{
    public class Auditoria
    {
        public int id {  get; set; }
        public String? Desc { get; set; }
        public DateTime fecha { get; set; }
        public String? tablaAfectada { get; set; }
    }
}
   