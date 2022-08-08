using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Ubicaciones")]
    public class Ubicacion
    {
        [Key]
        [Display(Name = "Codigo Divipola")]
        public int CodigoLugar { get; set; }

        [Display(Name = "Nombre")]
        public string Lugar { get; set; }
    }
}
