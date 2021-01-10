using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Personas")]
    public class Persona
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(200)]
        public string Nombres { get; set; }

        [MaxLength(200)]
        public string Apellidos { get; set; }

        public long TipoDocumentoId { get; set; }

        public TipoDocumento TipoDocumento { get; set; }

        [MaxLength(20)]
        public string NumeroDocumento { get; set; }

        [MaxLength(500)]
        public string Direccion { get; set; }

        public string Ciudad { get; set; }

        public int Telefono { get; set; }

        public bool Estado { get; set; }

        public Guid? UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
