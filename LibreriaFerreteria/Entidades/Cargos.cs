using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class Cargos
    {
        public int id {  get; set; }
        public String? nombre { get; set; }
        [NotMapped] public List<Salarios>? Salarios { get; set; }
    }
}
   