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

        [ForeignKey("ProcedimientoId")]
        public Procedimiento Procedimientos { get; set; }

        [Required]
        public Guid RepuestoId { get; set; }

        [ForeignKey("RepuestoId")]
        public Repuesto Repuesto { get; set; }

        [Required]
        public int Cantidad { get; set; }

    }
}
