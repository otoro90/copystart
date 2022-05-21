using CopyStart.Entities;
using System;

namespace CopyStart.Areas.Administracion.Models
{
    public class ProcedimientoCrear: Parametrica
    {

        public Guid TipoServicioId { get; set; }

            public int Numero { get; set; }
    }
}
