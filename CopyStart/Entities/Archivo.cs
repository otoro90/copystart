using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Archivos")]
    public class Archivo
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

        [NotMapped]
        public IFormFile File { get; set; }
    }
}
