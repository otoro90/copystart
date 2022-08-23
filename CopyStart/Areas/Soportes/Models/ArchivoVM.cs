using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace CopyStart.Areas.Soportes.Models
{
    public class ArchivoVM
    {
        [Required]
        [DataType(DataType.Upload)]
       /* [FileExtensions(Extensions = "application/pdf,.pdf,pdf")]*/
        public IFormFile File { get; set; }
    }
}