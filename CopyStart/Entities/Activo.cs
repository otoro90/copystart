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
        
        [Required]
        public string Serial { get; set; }

        [Required]
        public Guid TipoActivoId { get; set; }

        [ForeignKey("TipoActivoId")]
        public TipoActivo TipoActivo { get; set; }

            
        [MaxLength(512)]
        public string Descripcion { get; set; }

        [Required]
        public string Marca { get; set; }
      
        [Required]
        [MaxLength(100)]
        public string Modelo { get; set; }

        public string Ubicacion { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        public Guid PersonaId { get; set; }

       [ForeignKey("PersonaId")]
       public Persona Persona { get; set; }
    
    }
}
