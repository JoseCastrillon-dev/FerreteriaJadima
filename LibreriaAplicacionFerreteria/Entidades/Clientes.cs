using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaAplicacionFerreteria.Entidades
{
    public class Clientes
    {
        public int id {  get; set; }
        public int id_persona { get; set; }

        [NotMapped] public List<Ventas>? Ventas { get; set; }
        [ForeignKey("id_persona")] public Personas? _persona { get; set; }

    }
}
