using System;
using System.ComponentModel.DataAnnotations;

namespace CopyStart.Entities
{
    public class Parametrica
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(1024)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required]
        [MaxLength(5)]
        public string Codigo { get; set; }

        public bool Estado { get; set; }
    }
}
