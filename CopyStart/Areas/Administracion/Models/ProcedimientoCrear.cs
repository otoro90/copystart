using CopyStart.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace CopyStart.Areas.Administracion.Models
{
    public class ProcedimientoCrear: Parametrica
    {
        [Display(Name = "Tipo de Servicio")]
        public Guid TipoServicioId { get; set; }

        
        public Guid ProcedimientoId { get; set; }

        public int Numero { get; set; }

        [Display(Name = "Tiempo de ejecución (minutos)")]
        public int TiempoEjecucion { get; set; }
    }
}
