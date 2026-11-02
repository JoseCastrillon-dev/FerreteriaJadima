using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class TiposDePagos
    {
        public int id {  get; set; }
        public String? metodo { get; set; }

        public List<Cuotas>? Cuotas { get; set; }
        [NotMapped] public List<Salarios>? Salarios { get; set; }
        public List<Ventas>? Ventas { get; set; }
    }
}
