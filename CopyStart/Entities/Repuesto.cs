using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Repuestos")]
    public class Repuesto : Parametrica
    {
        
        public string Marca { get; set; }
           
        public string Modelo { get; set; }
    }
}
