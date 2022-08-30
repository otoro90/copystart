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
        [Display(Name = "Nombres", Prompt = "Ingrese nombres de la persona")]
        public string Nombres { get; set; }

        [MaxLength(200)]
        [Display(Name = "Apellidos", Prompt = "Ingrese apellidos de la persona")]
        public string Apellidos { get; set; }

        [Display(Name = "Tipo de documento")]
        public Guid TipoDocumentoId { get; set; }

        [Display(Name = "Tipo de documento")]
        [ForeignKey("TipoDocumentoId")]
        public TipoDocumento TipoDocumento { get; set; }

        [Display(Name = "Documento de identidad", Prompt ="Ingrese número de documento")]
        [MaxLength(20)]
        public string NumeroDocumento { get; set; }

        [MaxLength(500)]
        [Display(Name = "Dirección de residencia", Prompt ="Ingrese su direccion")]
        public string Direccion { get; set; }

        [Display(Name = "Ubicación")]       
        public string UbicacionId { get; set; }
        
        [ForeignKey("UbicacionId")]
        [Display(Name = "Ubicación")]
        public Ubicacion Ubicacion { get; set; }

        [Display(Name = "Teléfono o celular", Prompt ="Ingrese número telefónico de contacto")]
        public long Telefono { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
