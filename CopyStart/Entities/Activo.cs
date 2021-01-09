using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CopyStart.Entities
{
    [Table("Activos")]
    public class Activo
    {
        [Key]
        public long Id { get; set; }

        public long Codigo { get; set; }

        public string TipoActivo { get; set; }

        public string Descripcion { get; set; }

        public string Marca { get; set; }
      
        public string Modelo { get; set; }

        public string Ubicacion { get; set; }

        public DateTime FechaRegistro { get; set; }

        public long UsuarioId { get; set; }

       [ForeignKey("UsuarioId")]
       public Usuario Usuario { get; set; }
    
    }
}
