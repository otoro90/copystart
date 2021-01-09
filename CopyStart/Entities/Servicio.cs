using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    class Servicio
    {
        [Key]
        public long Id { get; set; }

        public string Estado { get; set; } 

        public DateTime FechaRealizacion { get; set; }

        public long DiagnosticoId { get; set; }
        [ForeignKey ("DiagnosticoId")]
        public Diagnostico Diagnostico { get; set; }
        
        public long ActivoId { get; set; }
        [ForeignKey("ActivoId")]
        public Activo Activo { get; set; }

        public long SolicitudId { get; set; }
        [ForeignKey("SolicitudId")]
        public Solicitud Solicitudes { get; set; }

        public long SoportesId { get; set; }
        [ForeignKey("SoportesId")]
        public Soporte Soportes { get; set; }
    }
}
