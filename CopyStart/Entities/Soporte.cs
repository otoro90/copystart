using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Soportes")]
    public class Soporte
    {
        [Key]
        public Guid Id { get; set; }

        public Guid DocumentoReciboId { get; set; }

        [Display(Name = "Recibo")]
        [ForeignKey("ReciboId")]
        public Documento DocumentoRecibo { get; set; }

        [Required]
        public Guid CopiaFacturaFirmadaId { get; set; }

        [Display(Name = "Factura")]
        [ForeignKey("DocumentoFacturaId")]
        public Documento CopiaFacturaFirmada { get; set; }

        [Required]
        public Guid DocumentoCertificacionId { get; set; }

        [Display(Name = "Documento")]
        [ForeignKey("DocumentoCertificacionId")]
        public Documento DocumentoCertificacion { get; set; }
    }
}
