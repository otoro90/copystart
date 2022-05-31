using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Diagnosticos")]
    public class Diagnostico
    {
       


        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(4096)]
        public string Descripción { get; set; }

        [Display(Name = "Fecha de Diagnostico")]
        [Required]
        public DateTime FechaDiagnostico { get; set; }

        public Guid? SoportesId { get; set; }
        
        [Display(Name = "Soportes")]
        [ForeignKey("SoportesId")]
        public Soporte Soportes { get; set; }

    }
}
