using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Documentos")]
    public class Documento
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        //Ejemplo: pdf, jpg, itf ...
        [Required]
        [MaxLength(5)]
        public string Tipo { get; set; }

        //Peso en Kbytes
        [Required]
        public double? Peso { get; set; }
    }
}
