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
        
       
        [MaxLength(150)]
        public string Incidencia { get; set; }

        
        [MaxLength(1024)]
        public string Descripcion { get; set; }

        public int UbicacionId { get; set; }

        [ForeignKey("UbicacionId")]
        public Ubicacion Ubicacion { get; set; }


        [Display(Name = "Fecha")]
        
        public DateTime FechaSolicitud { get; set; }

        [Display(Name = "Estado")]
        
        public EstadosSolicitud Estado { get; set; }

        public Guid? TecnicoId { get; set; }
        
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

    public enum EstadosSolicitud
    {


    }
}
