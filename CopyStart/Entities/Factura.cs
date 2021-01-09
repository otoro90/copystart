using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Facturas")]
    public class Factura
    {
        [Key]
        public Guid Id { get; set; }
    }
}
