using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    class Soporte
    {
        [Key]
        public long Id { get; set; }

        public long ReciboId { get; set; }
        [ForeignKey("ReciboId")]
        public Recibo Recibo { get; set; }
        
        public long FacturaId { get; set; }
        [ForeignKey("FacturaId")]
        public Factura Factura { get; set; }

        public long CertificacionId { get; set; }
        [ForeignKey("CertificacionId")]
        public Certificacion Certificacion { get; set; }
    }
}
