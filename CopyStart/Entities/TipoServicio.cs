using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("TiposServicio")]
    public class TipoServicio : Parametrica
    {
        [Required]
        public Guid? TipoActivoId { get; set; }
        
        
        [Display(Name = "Tipo de Activo")]
        [ForeignKey("TipoActivoId")]
        public TipoActivo TipoActivo { get; set; }
    }
}
