using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("Solicitudes")]
    public class Solicitud
    {
        public Solicitud()
        {

            FechaSolicitud = DateTime.Now;

        }
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(150)]
        public string Incidencia { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Descripcion { get; set; }

        public string Ubicacion { get; set; }

        [Display(Name = "Fecha")]
        [Required]
        public DateTime FechaSolicitud { get; set; }

        [Display(Name = "Estado")]
        [Required]
        public string EstadoSolicitud { get; set; }

        public Guid TecnicoId { get; set; }
        
        [Display(Name = "Tecnico")]
        [ForeignKey("TecnicoId")]
        public Persona Tecnico { get; set; }
       
        public Guid ClienteId { get; set; }

        [Display(Name = "Cliente")]
        [ForeignKey("ClienteId")]
        public Persona Cliente { get; set; }

        public Guid ActivoId { get; set; }

        [Display(Name = "Activo")]
        [ForeignKey("ActivoId")]
        public Activo Activo { get; set; }

    }
}
