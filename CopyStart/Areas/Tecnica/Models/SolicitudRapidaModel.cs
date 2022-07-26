using CopyStart.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Areas.Tecnica.Models
{
    public class SolicitudRapidaModel
    {     
        public Persona Persona { get; set; }
        public Activo Activo { get; set; }
        public Solicitud Solicitud { get; set; }

    }
}
