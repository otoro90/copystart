using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Diagnosticos")]
    public class Diagnostico
    {
        public Diagnostico()
        {

            FechaDiagnostico = DateTime.Now;

        }


        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(4096)]
        public string Descripción { get; set; }

        [Display(Name = "Fecha de Diagnostico")]
        [Required]
        public DateTime FechaDiagnostico { get; set; }


        [Display(Name = "Tipo de Servicio")]
        public Guid TipoServicioId { get; set; }

        [ForeignKey("TipoServicioId")]
        public TipoServicio TipoServicio { get; set; }

        [Display(Name = "Soportes")]
        public Guid SoportesId { get; set; }
        [ForeignKey("SoportesId")]
        public Soporte Soportes { get; set; }

    }
}
