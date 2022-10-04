using CopyStart.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CopyStart.Areas.Tecnica.Models
{
    public class DetallesServicio
    {
        public Servicio Servicio { get; set; }
       
        public List<Archivo> Archivos { get; set; }


    }


}

