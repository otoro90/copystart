using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Procedimientos")]
    public class Procedimiento : Parametrica
    {
        [Display(Name = "Tiempo de ejecucion")]
        public string TiempoEjecucion { get; set; }


    }
}
