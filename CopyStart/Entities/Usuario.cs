using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CopyStart.Entities
{
    public class Usuario
    {
       [Key]
       public long Id { get; set; }
        
        public string UsuarioNombre { get; set; }


        public string Contraseña { get; set; }
    }
}
