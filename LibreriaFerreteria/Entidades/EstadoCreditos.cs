using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class EstadoCreditos
    {
        public int id { get; set; }
        public String? estado { get; set; }

        public List<Creditos>? Creditos { get; set; }
    }
}
