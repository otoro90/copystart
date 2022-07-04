using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Activos")]
    public class Activo
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Serial { get; set; }
        [Required]
        public Guid TipoActivoId { get; set; }

        [Display(Name = "Tipo de activo")]
        [ForeignKey("TipoActivoId")]
        public TipoActivo TipoActivo { get; set; }

        [MaxLength(512)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "Marca de activo")]
        public Guid MarcaActivoId { get; set; }

        [Display(Name = "Marca de activo")]
        [ForeignKey("MarcaActivoId")]
        public MarcaActivo MarcaActivo { get; set; }

        [Required]
        [Display(Name = "Modelo de activo")]
        public Guid ModeloActivoId { get; set; }

        [ForeignKey("ModeloActivoId")]
        [Display(Name = "Modelo de activo")]
        public ModeloActivo ModeloActivo { get; set; }

        public string Estado { get; set; }

        [Display(Name = "Fecha de Registro")]
        [Required]
        public DateTime FechaRegistro { get; set; }

        [Display(Name = "Cliente")]
        public Guid PersonaId { get; set; }

        [Display(Name = "Cliente")]
        [ForeignKey("PersonaId")]
        public Persona Persona { get; set; }
    }


}
