using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    class Solicitud
    {
        [Key]
        public long Id { get; set; }

        public string Incidencia { get; set; }

        public string Descripcion { get; set; }

        public string Ubicacion { get; set; }

        public DateTime FechaSolicitud { get; set; }

        public string EstadoSolicitud { get; set; }

        public long TecnicoId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Tecnico { get; set; }
       
        public long ClienteId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Cliente { get; set; }

        public long ActivoId { get; set; }
        [ForeignKey("ActivoId")]
        public Activo Activo { get; set; }

    }
}
