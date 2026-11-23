using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaAplicacionFerreteria.Entidades
{
    public class Permisos
    {
        public int id {  get; set; }
        public int id_rol { get; set; }
        public Boolean ver { get; set; }
        public Boolean guardar { get; set; }
        public Boolean borrar { get; set; }
    }
}
   