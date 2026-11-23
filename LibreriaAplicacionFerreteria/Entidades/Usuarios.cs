using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaAplicacionFerreteria.Entidades
{
    public class Usuarios
    {
        public int id { get; set; }
        public String? Usuario { get; set; }
        public String? Contraseña { get; set; }
    }
}
   