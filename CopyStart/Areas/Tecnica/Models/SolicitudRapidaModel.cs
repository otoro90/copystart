using CopyStart.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Areas.Tecnica.Models
{
    public class SolicitudRapidaModel
    {     

        [Display(Name = "Tipo de Documento")]
        public Guid TipoDocumentoId { get; set; }

        [Display(Name = "Tipo de Documento")]
        [ForeignKey("TipoDocumentoId")]
        public TipoDocumento TipoDocumento { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(20)]
        [Required]
        public string NumeroDocumento { get; set; }

        [Required]
        public string Serial { get; set; }

        [Required]
        public Guid TipoActivoId { get; set; }

        [Display(Name = "Tipo de activo")]
        [ForeignKey("TipoActivoId")]
        public TipoActivo TipoActivo { get; set; }

       
        [Display(Name = "Marca de activo")]
        public Guid MarcaActivoId { get; set; }

        [Display(Name = "Marca de activo")]
        [ForeignKey("MarcaActivoId")]
        public MarcaActivo MarcaActivo { get; set; }

       
        [Display(Name = "Modelo de activo")]
        public Guid ModeloActivoId { get; set; }

        [ForeignKey("ModeloActivoId")]
        [Display(Name = "Modelo de activo")]
        public ModeloActivo ModeloActivo { get; set; }

        public int UbicacionId { get; set; }

        [ForeignKey("UbicacionId")]
        [Display(Name = "Ubicación")]
        public Ubicacion Ubicacion { get; set; }

        public string Direccion { get; set; }

        [MaxLength(150)]
        [Required]
        public string Incidencia { get; set; }


    }
}
