using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaAplicacionFerreteria.Entidades
{
    public class Tipos
    {
        public int id {  get; set; }
        public String? nombre {  get; set; }

        [NotMapped] public List<Productos>? Productos { get; set; }
    }
}
    