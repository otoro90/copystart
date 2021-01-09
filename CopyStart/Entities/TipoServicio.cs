using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CopyStart.Entities
{
    class TipoServicio
    {
        [Key]
        public long Id { get; set; }

        public string Nombre { get; set; }
    }
}
