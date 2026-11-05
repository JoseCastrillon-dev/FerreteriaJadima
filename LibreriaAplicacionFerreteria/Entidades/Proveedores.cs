using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaAplicacionFerreteria.Entidades
{
    public class Proveedores
    {
        public int id {  get; set; }
        public String? nombre { get; set; }
        public String? numeroDeTelefono { get; set; }
        public String? direccion {  get; set; }
        public String? email { get; set; }

        [NotMapped] public List<Compras>? Compras { get; set; }
    }
}
