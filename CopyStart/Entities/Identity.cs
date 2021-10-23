using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CopyStart.Entities
{
    
    public class ApplicationUser : IdentityUser
    {
        public Guid PersonaId { get; set; }

        [Display(Name = "Persona")]
        [ForeignKey("PersonaId")]
        public Persona Persona { get; set; }
        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
    }

    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }

    public class ApplicationUserClaim : IdentityUserClaim<Guid>
    {
        public virtual ApplicationUser User { get; set; }
    }

    public class ApplicationUserLogin : IdentityUserLogin<Guid>
    {
        public virtual ApplicationUser User { get; set; }
    }

    public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
    {
        public virtual ApplicationRole Role { get; set; }
    }

    public class ApplicationUserToken : IdentityUserToken<Guid>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
