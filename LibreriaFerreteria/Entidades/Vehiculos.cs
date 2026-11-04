using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class Vehiculos
    {
        public int id {  get; set; }
        public String? matricula { get; set; }
        public String? marca { get; set; }
        public String? numeroDeChasis { get; set; }
        public String? numeroMotor { get; set; }

        [NotMapped] public List<Domicilios>? Domicilios { get; set; }
    }
}
