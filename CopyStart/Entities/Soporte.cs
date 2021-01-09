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

        public Guid ReciboId { get; set; }
        [ForeignKey("ReciboId")]
        public Recibo Recibo { get; set; }
        
        public Guid FacturaId { get; set; }
        [ForeignKey("FacturaId")]
        public Factura Factura { get; set; }

        public Guid CertificacionId { get; set; }
        [ForeignKey("CertificacionId")]
        public Certificacion Certificacion { get; set; }
    }
}
