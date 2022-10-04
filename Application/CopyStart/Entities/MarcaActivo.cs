using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{

    [Table("MarcaActivos")]
    public class MarcaActivo: Parametrica
    {
        [Required]
        [Display(Name = "Tipo de activo")]
        public Guid TipoActivoId { get; set; }

       
        [ForeignKey("TipoActivoId")]
        [Display(Name = "Tipo de activo")]
        public TipoActivo TipoActivo { get; set; }

    }
}
