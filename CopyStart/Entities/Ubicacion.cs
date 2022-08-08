using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Ubicaciones")]
    public class Ubicacion
    {
        [Key]
        [Display(Name = "Código de municipio", Prompt ="Ingrese código de departamento")]
        public string CodigoMunicipio { get; set; }

        [Display(Name ="Código de departamento", Prompt = "Ingrese código de municipio")]
        public string CodigoDepartamento { get; set; }

        [Display(Name = "Departamento", Prompt = "Ingrese nombre de departamento")]
        public string Departamento { get; set; }

        [Display(Name = "Municipio", Prompt = "Ingrese nombre de municipio")]
        public string Municipio { get; set; }

        [Display(Name = "Latitud", Prompt = "Ingrese nombre de latitud")]
        public string Latitud { get; set; }

        [Display(Name = "Longitud", Prompt = "Ingrese nombre de longitud")]
        public string Longitud { get; set; }
    }
}
