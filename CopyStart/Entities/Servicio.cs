using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Servicios")]
    public class Servicio
    {
        [Key]
        public long Id { get; set; }

        [Display(Name = "Estado actual")]
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
        [Required]
        public long SolicitudId { get; set; }

        [Display(Name = "Solicitud")]
        [ForeignKey("SolicitudId")]
        public Solicitud Solicitudes { get; set; }

        [Display(Name = "Tipo de Servicio")]
        [Required]
        public Guid? TipoServicioId { get; set; }

        [Display(Name = "Tipo de Servicio")]
        [ForeignKey("TipoServicioId")]
        public TipoServicio TipoServicios { get; set; }

        [Display(Name = "Observaciones finales", Prompt = "Ingrese observaciones, si las hay")]
        public string Observaciones { get; set; }


        public IEnumerable<ServicioProcedimientoTipoServicio> ProcedimientosRealizados { get; set; }

        public ICollection<ArchivoServicio> Archivos { get; set; }
    }
}

