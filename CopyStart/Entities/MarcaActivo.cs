using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CopyStart.Entities
{

    [Table("MarcaActivos")]
    public class MarcaActivo: Parametrica
    {
        [Required]
        public Guid TipoActivoId { get; set; }

       
        [ForeignKey("TipoActivoId")]
        public TipoActivo TipoActivo { get; set; }

    }
}
