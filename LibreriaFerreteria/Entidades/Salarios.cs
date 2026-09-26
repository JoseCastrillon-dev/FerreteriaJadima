using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class Salarios
    {
        public int id {  get; set; }
        public int id_cargo { get; set; }
        public int id_tiposDePago { get; set; }
        public decimal valorHora { get; set; }
        public int horasTrabajadas { get; set; }

        [ForeignKey("cargo")] public Cargos? _cargo { get; set; }
        [ForeignKey("tiposDePago")] public TiposDePagos? _tiposDePago { get; set; }
    }
}
