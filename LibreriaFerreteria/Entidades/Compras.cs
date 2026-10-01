using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class Compras
    {
        public int id {  get; set; }
        public int id_proveedor { get; set; }
        public int id_empleado { get; set; }
        public DateTime fechaCompra { get; set; }
        public decimal total {  get; set; }
        public List<DetalleCompras>? DetalleCompras { get; set; }

        [ForeignKey("id_proveedor")] public Proveedores? _proveedor { get; set; }
        [ForeignKey("id_empleado")] public Empleados? _empleado { get; set; }
    }
}
