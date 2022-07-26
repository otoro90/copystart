using System;
using System.Collections.Generic;
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

        [Display(Name = "Tipo de Documento")]
        public Guid TipoDocumentoId { get; set; }

        [Display(Name = "Tipo de Documento")]
        [ForeignKey("TipoDocumentoId")]
        public TipoDocumento TipoDocumento { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(20)]
        public string NumeroDocumento { get; set; }

        [MaxLength(500)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Display(Name = "Ubicación")]       
        public int UbicacionId { get; set; }
        
        [ForeignKey("UbicacionId")]
        [Display(Name = "Ubicación")]
        public Ubicacion Ubicacion { get; set; }

        [Display(Name = "Teléfono")]
        public long Telefono { get; set; }

        public string Estado { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
