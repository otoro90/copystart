using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CopyStart.Entities
{
    [Table("ServiciosProcedimientos")]
    public class ServicioProcedimiento
    {
        public Guid Id {get; set;}

        public Guid ServicioId {get; set;}
        [ForeignKey("ServicioId")]
        public Servicio Servicio {get; set;}

        public Guid ProcedimientoId {get; set;}
        [ForeignKey("ProcedimientoId")]
        public Procedimiento Procedimiento {get; set;}

        public string ProcedimientosRealizados {get; set;}
    }
}
