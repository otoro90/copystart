using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Repuestos")]
    public class Repuesto
    {
        [Key]
        public Guid Id { get; set; }

        public long Codigo { get; set; }

        public string Nombre { get; set; }
        
        public string Descripcion { get; set; }
        
        public string Marca { get; set; }
           
        public string Modelo { get; set; }
    }
}
