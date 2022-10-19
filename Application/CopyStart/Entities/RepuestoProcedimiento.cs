using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("RepuestosProcedimientos")]
    public class RepuestoProcedimiento
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Procedimiento")]
        public Guid ProcedimientoId { get; set; }

        [ForeignKey("ProcedimientoId")]
        [Display(Name = "Procedimiento")]
        public Procedimiento Procedimientos { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Repuesto")]
        public Guid RepuestoId { get; set; }

        [ForeignKey("RepuestoId")]
        [Display(Name = "Repuesto")]
        public Repuesto Repuesto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Cantidad { get; set; }
    }
}
