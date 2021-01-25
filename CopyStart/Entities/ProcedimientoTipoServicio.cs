using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CopyStart.Entities
{
    [Table("ProcedimientosTipoServicios")]
    public class ProcedimientoTipoServicio
    {
        [Key]
        public Guid ProcedimientoId { get; set; }

        [Required]
        public int Numero { get; set; }

        [Display(Name = "Procedimientos")]
        [ForeignKey("ProcedimientoId")]
        public Procedimiento Procedimientos { get; set; }

        public Guid TipoServicioId { get; set; }
       
        [Display(Name = "Tipo de Servicio")]
        [ForeignKey("TipoServicioId")]
        public TipoServicio TipoServicio { get; set; }


    }
}
