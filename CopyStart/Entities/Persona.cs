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


        public Guid TipoDocumentoId { get; set; }

        [Display(Name = "Tipo de Documento")]
        [ForeignKey("TipoDocumentoId")]
        public TipoDocumento TipoDocumento { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(20)]
        public string NumeroDocumento { get; set; }

        [MaxLength(500)]
        public string Direccion { get; set; }

        public string Ciudad { get; set; }
                
        public int Telefono { get; set; }

        public bool Estado { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
