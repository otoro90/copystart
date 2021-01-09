using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace CopyStart.Entities
{
    class Repuesto
    {
        [Key]
        public long Id { get; set; }

        public long Codigo { get; set; }

        public string Nombre { get; set; }
        
        public string Descripcion { get; set; }
        
        public string Marca { get; set; }
           
        public string Modelo { get; set; }
    }
}
