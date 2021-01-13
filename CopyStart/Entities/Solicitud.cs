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
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(150)]
        public string Incidencia { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Descripcion { get; set; }

        public string Ubicacion { get; set; }
        
        [Required]
        public DateTime FechaSolicitud { get; set; }

        [Required]
        public string EstadoSolicitud { get; set; }

        public Guid TecnicoId { get; set; }
        [ForeignKey("TecnicoId")]
        public Persona Tecnico { get; set; }
       
        public Guid ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Persona Cliente { get; set; }

        public Guid ActivoId { get; set; }
        [ForeignKey("ActivoId")]
        public Activo Activo { get; set; }

    }
}
