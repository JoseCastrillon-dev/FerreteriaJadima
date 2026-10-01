using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class Domicilios
    {
        public int id { get; set; }
        public int id_venta { get; set; }
        public int id_vehiculo { get; set; }
        public String? numeroDeTelefono { get; set; }
        public String? direccion { get; set; }

        public List<Entregas>? Entregas { get; set; }

        [ForeignKey("id_venta")] public Ventas? _venta { get; set; }
        [ForeignKey("id_vehiculo")] public Vehiculos? _vehiculos { get; set; }
    }
}
