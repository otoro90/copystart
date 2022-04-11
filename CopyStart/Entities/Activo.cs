using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Activos")]
    public class Activo
    {   
        public Activo()
        {

            FechaRegistro = DateTime.Now;

        }
        
        [Key]
        public Guid Id { get; set; }

        
        [Required]
        public string Serial { get; set; }

        
        [Required]
        public Guid TipoActivoId { get; set; }

        [Display(Name = "Tipo de Activo")]
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

        public string Estado { get; set; }

        [Display(Name = "Fecha de Registro")]
        [Required]
        public DateTime FechaRegistro { get; set; }

        
        public Guid PersonaId { get; set; }

        [Display(Name = "Cliente")]
        [ForeignKey("PersonaId")]
       public Persona Persona { get; set; }
    
    }
}
