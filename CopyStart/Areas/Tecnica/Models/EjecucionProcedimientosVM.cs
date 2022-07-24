using CopyStart.Entities;
using System;
using System.Collections.Generic;

namespace CopyStart.Areas.Tecnica.Models
{
    public class EjecucionProcedimientosVM
    {
        public string Id { get; set; }
        public string ServicioId { get; set; }
        public string ProcedimientoTipoServicioId { get; set; }
        public bool ProcedimientosRealizados { get; set; }
        public string Observaciones { get; set; }
    }
}
