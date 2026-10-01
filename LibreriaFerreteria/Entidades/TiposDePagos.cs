using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class TiposDePagos
    {
        public int id {  get; set; }
        public String? metodo { get; set; }

        public List<Cuotas>? Cuotas { get; set; }
        public List<Salarios>? Salarios { get; set; }
        public List<Ventas>? Ventas { get; set; }
    }
}
