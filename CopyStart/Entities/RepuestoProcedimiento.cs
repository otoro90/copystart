using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("RepuestosProcedimientos")]
    class RepuestoProcedimiento
    {
        [Key]
        public Guid Id { get; set; }

        public long ProcedimientoId { get; set; }
        [ForeignKey("ProcedimientoId")]
        public Procedimiento Procedimientos { get; set; }

        public long RepuestoId { get; set; }
        [ForeignKey("RepuestoId")]
        public Repuesto Repuesto { get; set; }
       
        public int Cantidad { get; set; }

    }
}
