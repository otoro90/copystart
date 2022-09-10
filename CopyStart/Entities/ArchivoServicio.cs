using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopyStart.Entities
{
    public class ArchivoServicio
    {
        [Key, Column(Order = 0)]
        public Guid ArchivoId { get; set; }

        [Display(Name = "Archivo")]
        [ForeignKey("ArchivoId")]
        public Archivo Archivo { get; set; }


        [Key, Column(Order = 1)]
        public long ServicioId { get; set; }

        [Display(Name = "Servicio")]
        [ForeignKey("ServicioId")]
        public Servicio Servicio { get; set; }

       
    }
}
