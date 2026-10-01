using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class Creditos
    {
        public int id { get; set; }
        public int idVenta { get; set; }
        public int id_estadoCredito { get; set; }
        public DateTime? fechaPrestamo { get; set; }
        public decimal valorPrestamo { get; set; }

        public List<Cuotas>? Cuotas { get; set; }

        [ForeignKey("idVenta")] public Ventas? _venta { get; set; }
        [ForeignKey("id_estadoCredito")] public EstadoCreditos? _estadoCredito { get; set; }
    }
}
