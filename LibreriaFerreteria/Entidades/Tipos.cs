using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class Tipos
    {
        public int id {  get; set; }
        public String? nombre {  get; set; }

        public List<Productos>? Productos { get; set; }
    }
}
    