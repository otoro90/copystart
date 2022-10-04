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
        [Display(Name = "Nombre", Prompt = "Ingrese nombre de la parametrica")]
        public string Nombre { get; set; }

        [MaxLength(1024)]
        [Display(Name = "Descripción", Prompt = "Ingrese descripcion de la parametrica")]
        public string Descripcion { get; set; }

        [Required]
        [MaxLength(5)]
        [Display(Name = "Codigo identificador", Prompt ="Ingrese el codigo identificador")]
        public string Codigo { get; set; }

        [Display(Name = "Estado")]
        public bool Estado { get; set; }
    }
}
