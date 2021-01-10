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

        public Guid? DocumentoReciboId { get; set; }
        [ForeignKey("ReciboId")]
        public Documento DocumentoRecibo { get; set; }
        
        public Guid CopiaFacturaFirmadaId { get; set; }
        [ForeignKey("DocumentoFacturaId")]
        public Documento CopiaFacturaFirmada { get; set; }

        public Guid DocumentoCertificacionId { get; set; } 
        [ForeignKey("DocumentoCertificacionId")]
        public Documento DocumentoCertificacion { get; set; }
    }
}
