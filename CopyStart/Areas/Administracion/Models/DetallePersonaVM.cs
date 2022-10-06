using CopyStart.Entities;
using System.Collections.Generic;

namespace CopyStart.Areas.Administracion.Models
{
    public class DetallePersonaVM
    {
        public Persona Persona { get; set; }
        public List<ApplicationUserRole> RolesUsuario { get; set; }
    }
}
