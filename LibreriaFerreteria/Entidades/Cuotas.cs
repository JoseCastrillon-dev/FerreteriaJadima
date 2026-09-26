using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class Cuotas
    {
        public int id { get; set; }
        public int id_credito { get; set; }
        public int id_tipoDePago { get; set; }
        public DateTime? fechaDePago { get; set; }
        public decimal valorDeCuota { get; set; }
        public int numeroDeCuota { get; set; }

        [ForeignKey("id_credito")] public Creditos? _credito { get; set; }
        [ForeignKey("id_tipoDePago")] public TiposDePagos? _tipoDePago { get; set; }
    }
}
