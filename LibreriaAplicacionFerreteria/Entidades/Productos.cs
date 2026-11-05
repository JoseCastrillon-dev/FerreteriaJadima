using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaAplicacionFerreteria.Entidades
{
    public class Productos
    {
        public int id { get; set; }
        public int id_tipo { get; set; }
        public String? codigo { get; set; }
        public int cantidad { get; set; }
        public String? nombre { get; set; }
        public decimal valor { get; set; }

        [NotMapped] public List<DetalleCompras>? DetalleCompras { get; set; }
        [NotMapped] public List<DetalleVentas>? DetalleVentas { get; set; }
        [NotMapped] public List<Estanterias>? Estanterias { get; set; }
        [ForeignKey("id_tipo")] public Tipos? _tipo { get; set; }
    }
}
