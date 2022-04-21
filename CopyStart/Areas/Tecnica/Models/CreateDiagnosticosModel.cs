using CopyStart.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CopyStart.Areas.Tecnica.Models
{
    public class CreateDiagnosticosModel : Diagnostico
    {
        public Guid? SolicitudId { get; set; }
        
    }
}
