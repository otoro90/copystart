using CopyStart.Entities;
using System;
using System.Collections.Generic;

namespace CopyStart.Areas.Administracion.Models
{
    public class IndexPersona
    {
        public Persona Persona { get; set; }
        public ApplicationUser User { get; set; }

    }
}
