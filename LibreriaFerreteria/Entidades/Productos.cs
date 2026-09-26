using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class Productos
    {
        public int id { get; set; }
        public int id_tipo { get; set; }
        public String? codigo { get; set; }
        public int cantidad { get; set; }
        public String? nombre { get; set; }
        public decimal valor { get; set; }

        [ForeignKey("tipo")] public Tipos? _tipo { get; set; }
    }
}
