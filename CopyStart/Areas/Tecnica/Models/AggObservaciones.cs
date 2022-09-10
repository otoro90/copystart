using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CopyStart.Areas.Tecnica.Models
{
    public class AggObservaciones
    {
    public string Observaciones { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        /* [FileExtensions(Extensions = "application/pdf,.pdf,pdf")]*/
        public List<IFormFile> File { get; set; }
    }


}

