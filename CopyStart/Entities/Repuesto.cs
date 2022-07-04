using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Repuestos")]
    public class Repuesto : Parametrica
    {
        [Required]
        public string Marca { get; set; }
           
        [Required]
        [MaxLength(100)]
        public string Modelo { get; set; }
    }
}
