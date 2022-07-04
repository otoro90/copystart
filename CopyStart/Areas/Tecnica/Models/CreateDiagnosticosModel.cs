using CopyStart.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Areas.Tecnica.Models
{
    public class CreateDiagnosticosModel : Diagnostico
    {
        public long? SolicitudId { get; set; }

        [Display(Name = "Solicitud")]
        [ForeignKey("SolicitudId")]
        public Solicitud Solicitudes { get; set; }

        public Guid? TipoServicioId { get; set; }
        
    }
}
