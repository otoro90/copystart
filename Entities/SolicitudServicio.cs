using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public partial class SolicitudServicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SerialEquipo { get; set; }
        public string NombreCliente { get; set; }
        public long CelularCliente { get; set; }
        public string DireccionCliente { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaSolicitud { get; set; }
    }
}
