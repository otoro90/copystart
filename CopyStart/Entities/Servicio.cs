using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Servicios")]
    public class Servicio
    {
        [Key]
        public long Id { get; set; }
        public string Estado { get; set; }

        [Display(Name = "Fecha de inicio")]
        public DateTime? FechaInicio { get; set; }

        [Display(Name = "Fecha de finalización")]
        public DateTime? FechaFinalizacion { get; set; }

        [Display(Name = "Activo")]
        public long ActivoId { get; set; }

        [Display(Name = "Activo")]
        [ForeignKey("ActivoId")]
        public Activo Activo { get; set; }

        [Display(Name = "Solicitud")]
        public long SolicitudId { get; set; }

        [Display(Name = "Solicitud")]
        [ForeignKey("SolicitudId")]
        public Solicitud Solicitudes { get; set; }

        [Display(Name = "Soporte")]
        public Guid? SoportesId { get; set; }

        [Display(Name = "Soporte")]
        [ForeignKey("SoportesId")]
        public Soporte Soportes { get; set; }

        [Display(Name = "Tipo de Servicio")]
        public Guid? TipoServicioId { get; set; }

        [Display(Name = "Tipo de Servicio")]
        [ForeignKey("TipoServicioId")]
        public TipoServicio TipoServicios { get; set; }

        public string Observaciones { get; set; }
    }
}
