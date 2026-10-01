using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class Ventas
    {
        public int id { get; set; }
        public int id_detalleVenta { get; set; }
        public int id_tipoDePago { get; set; }
        public int id_empleado { get; set; }
        public int id_cliente { get; set; }
        public decimal iva { get; set; }
        public decimal total { get; set; }

        public List<Creditos>? Creditos { get; set; }
        public List<Domicilios>? Domicilios { get; set; }

        [ForeignKey("id_detalleVenta")] public DetalleVentas? _detalleVenta { get; set; }
        [ForeignKey("id_tipoDePago")] public TiposDePagos? _tipoDePago { get; set; }
        [ForeignKey("id_empleado")] public Empleados? _empleado { get; set; }
        [ForeignKey("id_cliente")] public Clientes? _cliente { get; set; }
    }
}
