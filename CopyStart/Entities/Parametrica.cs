using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    public class Parametrica
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [MaxLength(1024)]
        public string Descripcion { get; set; }

        [Required]
        [MaxLength(5)]
        public string Codigo { get; set; }

        public bool Estado { get; set; }
    }
}
