using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    class Procedimiento
    {
        [Key]
        public long Id { get; set; }

        public int Numero { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        

    }
}
