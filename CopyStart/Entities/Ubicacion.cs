using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CopyStart.Entities
{
    [Table("Ubicaciones")]
    public class Ubicacion
    {
        [Key]
       public int CodigoLugar { get; set; }

       public string Lugar { get; set; }

    }
}
