using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Ubicaciones")]
    public class Ubicacion
    {
        [Key]
        public string CodigoMunicipio { get; set; }
        public string CodigoDepartamento { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
    }
}
