using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("ModeloActivos")]

    public class ModeloActivo: Parametrica
    {

        [Required]
        [Display(Name = "Marca de activo")]
        public Guid MarcaActivoId { get; set; }


        [ForeignKey("MarcaActivoId")]
        [Display(Name = "Marca de activo")]
        public MarcaActivo MarcaActivo { get; set; }
    }
}
