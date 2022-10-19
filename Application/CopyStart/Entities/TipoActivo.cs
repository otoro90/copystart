using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("TiposActivo")]
    public class TipoActivo : Parametrica
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Marca de activo")]
        public Guid MarcaActivoId { get; set; }


        [ForeignKey("MarcaActivoId")]
        [Display(Name = "Marca de activo")]
        public MarcaActivo MarcaActivo{ get; set; }
    }
}
