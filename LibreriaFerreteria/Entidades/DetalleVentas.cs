using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class DetalleVentas
    {
        public int id {  get; set; }
        public int id_producto { get; set; }
        public int cantidad { get; set; }
        public decimal iva {  get; set; }
        public decimal total { get; set; }

        [ForeignKey("id_producto")] public Productos? _producto { get; set; }
    }
}
