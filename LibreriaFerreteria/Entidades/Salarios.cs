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
        public List<Empleados>? Empleados { get; set; }

        [ForeignKey("id_cargo")] public Cargos? _cargo { get; set; }
        [ForeignKey("id_tiposDePago")] public TiposDePagos? _tiposDePago { get; set; }
    }
}
