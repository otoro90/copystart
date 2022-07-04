using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Soportes")]
    public class Soporte
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Recibo")]
        public Guid DocumentoReciboId { get; set; }

        [ForeignKey("DocumentoReciboId")]
        [Display(Name = "Recibo")]
        public Documento DocumentoRecibo { get; set; }

        [Required]
        [Display(Name = "Factura")]
        public Guid CopiaFacturaFirmadaId { get; set; }

        [ForeignKey("DocumentoFacturaId")]
        [Display(Name = "Factura")]
        public Documento CopiaFacturaFirmada { get; set; }

        [Required]
        [Display(Name = "Documento")]
        public Guid DocumentoCertificacionId { get; set; }

        [ForeignKey("DocumentoCertificacionId")]
        [Display(Name = "Documento")]
        public Documento DocumentoCertificacion { get; set; }
    }
}
