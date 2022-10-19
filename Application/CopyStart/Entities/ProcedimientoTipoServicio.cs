using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("ProcedimientosTipoServicios")]
    public class ProcedimientoTipoServicio
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Número")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Procedimiento")]
        public Guid ProcedimientoId { get; set; }

        [Display(Name = "Procedimientos")]
        [ForeignKey("ProcedimientoId")]
        public Procedimiento Procedimientos { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Tipo de Servicio")]
        public Guid TipoServicioId { get; set; }

        [Display(Name = "Tipo de Servicio")]
        [ForeignKey("TipoServicioId")]
        public TipoServicio TipoServicio { get; set; }
    }
}
