using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Activos")]
    public class Activo
    {
        [Key]
        public Guid Id { get; set; }

        public long Codigo { get; set; }

        public string TipoActivo { get; set; }

        public string Descripcion { get; set; }

        public string Marca { get; set; }
      
        public string Modelo { get; set; }

        public string Ubicacion { get; set; }

        public DateTime FechaRegistro { get; set; }

        public long PersonaId { get; set; }

       [ForeignKey("PersonaId")]
       public Persona Persona { get; set; }
    
    }
}
