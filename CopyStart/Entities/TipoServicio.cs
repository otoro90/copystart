using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("TipoServicios")]
    public class TipoServicio
    {
        [Key]
        public long Id { get; set; }

        public string Nombre { get; set; }
    }
}
