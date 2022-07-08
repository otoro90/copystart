using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Solicitudes")]
    public class Solicitud
    {        
        [Key]
        public long Id { get; set; }        
       
        [MaxLength(150)]
        [Required]
        public string Incidencia { get; set; }
        
        [MaxLength(1024)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [MaxLength(1024)]
        public string Motivo { get; set; }
       

        [Display(Name = "Fecha de creación")]
        public DateTime FechaSolicitud { get; set; }

        [Display(Name = "Fecha de asignación")]
        public DateTime FechaAsignacion { get; set; }

        [Display(Name = "Estado")]        
        public string EstadoSolicitud { get; set; }

        [Display(Name = "Técnico")]
        public Guid? TecnicoId { get; set; }
        
        [Display(Name = "Técnico")]
        [ForeignKey("TecnicoId")]
        public Persona Tecnico { get; set; }

        [Display(Name = "Cliente")]
        public Guid ClienteId { get; set; }

        [Display(Name = "Cliente")]
        [ForeignKey("ClienteId")]
        public Persona Cliente { get; set; }

        [Display(Name = "Activo")]
        public long ActivoId { get; set; }

        [Display(Name = "Activo")]
        [ForeignKey("ActivoId")]
        public Activo Activo { get; set; }
    }
}
