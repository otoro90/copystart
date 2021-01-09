using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CopyStart.Entities
{
    class Persona
    {
        [Key]
        public long Id { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Direccion { get; set; }

        public string Ciudad { get; set; }

        public int Telefono { get; set; }

        public bool Estado { get; set; }
    }
}
