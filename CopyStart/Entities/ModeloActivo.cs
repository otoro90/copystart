using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CopyStart.Entities
{
    [Table("ModeloActivos")]

    public class ModeloActivo: Parametrica
    {

        [Required]
        public Guid MarcaActivoId { get; set; }


        [ForeignKey("MarcaActivoId")]
        public MarcaActivo MarcaActivo { get; set; }
    }
}
