using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CopyStart.Entities
{
    [Table("Activos")]
    class Diagnostico
    {
        [Key]
        public long Id { get; set; }

        public string Descripción { get; set; }

        public DateTime FechaDiagnostico { get; set; }
                
        public long TipoServicioId { get; set; }

        [ForeignKey("TipoServicioId")]
        public TipoServicio TipoServicio { get; set; }

        public long SoportesId { get; set; }
       [ForeignKey("SoportesId")]
       public Soporte Soportes { get; set; }

    }
}
