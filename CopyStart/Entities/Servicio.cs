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

        [Display(Name = "Fecha de Finalización")]
        public DateTime FechaRealizacion { get; set; }

        public Guid? DiagnosticoId { get; set; }

        [Display(Name = "Diagnostico")]
        [ForeignKey ("DiagnosticoId")]
        public Diagnostico Diagnostico { get; set; }
        
        public Guid ActivoId { get; set; }

        [Display(Name = "Activo")]
        [ForeignKey("ActivoId")]
        public Activo Activo { get; set; }

        public Guid SolicitudId { get; set; }

        [Display(Name = "Solicitud")]
        [ForeignKey("SolicitudId")]
        public Solicitud Solicitudes { get; set; }

        public Guid? SoportesId { get; set; }

        [Display(Name = "Soportes")]
        [ForeignKey("SoportesId")]
        public Soporte Soportes { get; set; }


        public Guid? TipoServicioId { get; set; }

        [Display(Name = "Tipo de Servicio")]
        [ForeignKey("TipoServicioId")]
        public TipoServicio TipoServicios { get; set; }
    }
}
