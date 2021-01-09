using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CopyStart.Entities
{
    class ProcedimientoTipoServicio
    {
        public long ProcedimientoId { get; set; }
        [ForeignKey("ProcedimientoId")]
        public Procedimiento Procedimientos { get; set; }
        
        public long TipoServicioId { get; set; }
        [ForeignKey("TipoServicioId")]
        public TipoServicio TipoServicio { get; set; }
    }
}
