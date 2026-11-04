using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class EstadoCreditos
    {
        public int id { get; set; }
        public String? estado { get; set; }

        [NotMapped] public List<Creditos>? Creditos { get; set; }
    }
}
