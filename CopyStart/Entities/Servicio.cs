using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Servicios")]
    public class Servicio
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Estado { get; set; } 

        public DateTime FechaRealizacion { get; set; }

        public Guid DiagnosticoId { get; set; }
        [ForeignKey ("DiagnosticoId")]
        public Diagnostico Diagnostico { get; set; }
        
        public Guid ActivoId { get; set; }
        [ForeignKey("ActivoId")]
        public Activo Activo { get; set; }

        public Guid SolicitudId { get; set; }
        [ForeignKey("SolicitudId")]
        public Solicitud Solicitudes { get; set; }

        public Guid SoportesId { get; set; }
        [ForeignKey("SoportesId")]
        public Soporte Soportes { get; set; }
    }
}
