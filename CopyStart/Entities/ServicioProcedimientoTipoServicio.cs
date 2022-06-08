using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    [Table("ServiciosProcedimientosTipoServicios")]
    public class ServicioProcedimientoTipoServicio
    {
        public Guid Id { get; set; }

        [Required]
        public long ServicioId { get; set; }

        [Display(Name = "Servicio")]
        [ForeignKey("ServicioId")]
        public Servicio Servicio { get; set; }

        [Required]
        public Guid ProcedimientoTipoServicioId { get; set; }

        [Display(Name = "Procedimiento")]
        [ForeignKey("ProcedimientoTipoServicioId")]
        public ProcedimientoTipoServicio ProcedimientoTipoServicio { get; set; }




        [Display(Name = "Procedimientos Realizados")]
        [Required]
        public bool ProcedimientosRealizados { get; set; }


        

        public string Observaciones { get; set; }


    }
}
