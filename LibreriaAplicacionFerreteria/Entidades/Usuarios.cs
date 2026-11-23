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
        public int rol { get; set; }
        public String? imagen { get; set; }

        [ForeignKey("rol")] public Roles? _roles { get; set; }
        [NotMapped] public bool Ver { get; set; }
        [NotMapped] public bool Guardar { get; set; }
        [NotMapped] public bool Borrar { get; set; }
    }
}
   