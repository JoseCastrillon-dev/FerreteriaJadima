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

        public List<Compras>? Compras { get; set; }
        public List<DetalleCompras>? DetalleCompras { get; set; }
        public List<DetalleVentas>? DetalleVentas { get; set; }
        public List<Estanterias>? Estanterias { get; set; }
        [ForeignKey("id_tipo")] public Tipos? _tipo { get; set; }
    }
}
