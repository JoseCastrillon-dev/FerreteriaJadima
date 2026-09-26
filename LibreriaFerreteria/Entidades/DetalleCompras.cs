using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class DetalleCompras
    {
        public int id { get; set; }
        public int id_compra { get; set; }
        public int id_producto { get; set; }
        public int cantidad { get; set; }
        public decimal preciosCompra { get; set; }

        [ForeignKey("id_compra")] public Compras? _compra { get; set; }
        [ForeignKey("id_producto")] public Productos? _producto { get; set; }
    }
}
