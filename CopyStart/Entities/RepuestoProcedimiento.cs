using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("RepuestosProcedimientos")]
    public class RepuestoProcedimiento
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ProcedimientoId { get; set; }

        [Display(Name = "Tipo de Servicio")]
        [ForeignKey("ProcedimientoId")]
        public Procedimiento Procedimientos { get; set; }

        [Required]
        public Guid RepuestoId { get; set; }

        [Display(Name = "Repuesto")]
        [ForeignKey("RepuestoId")]
        public Repuesto Repuesto { get; set; }

        [Required]
        public int Cantidad { get; set; }

    }
}
