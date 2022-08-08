using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Procedimientos")]
    public class Procedimiento : Parametrica
    {
        [Display(Name = "Tiempo de ejecución (minutos)", Prompt ="¿Cuanto tarda en realizarse este procedimiento?")]
        public int TiempoEjecucion { get; set; }
    }
}
