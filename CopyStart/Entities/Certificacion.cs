using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Certificaciones")]
    public class Certificacion
    {
        [Key]
        public Guid Id { get; set; }
    }
}
