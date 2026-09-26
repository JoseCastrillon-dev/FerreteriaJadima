using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibreriaFerreteria.Entidades
{
    public class Empleados
    {
        public int id {  get; set; }
        public int id_persona { get; set; }
        public int id_salario { get; set; }
        public String? carnet {  get; set; }
        public String? Genero { get; set; }

        [ForeignKey("id_persona")] public Personas? _persona { get; set; }
        [ForeignKey("id_salario")] public Salarios? _salario { get; set; }


    }
}
