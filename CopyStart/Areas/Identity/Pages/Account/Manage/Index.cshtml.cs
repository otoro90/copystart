using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CopyStart.Data;
using CopyStart.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CopyStart.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;



        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string Username { get; set; }


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public ModelInput PersonaInput { get; set; }

        public class ModelInput
        {
            [MaxLength(200)]
            
            public string Nombres { get; set; }

            [MaxLength(200)]
            public string Apellidos { get; set; }


            public Guid TipoDocumentoId { get; set; }

            [Display(Name = "Tipo de Documento")]
            [ForeignKey("TipoDocumentoId")]
            public TipoDocumento TipoDocumento { get; set; }

            [Display(Name = "Documento")]
            [MaxLength(20)]
            public string NumeroDocumento { get; set; }


            [MaxLength(500)]
            public string Direccion { get; set; }

            public string Ciudad { get; set; }

            public long Telefono { get; set; }



        }


        private async Task LoadAsync(ApplicationUser user)
                {
            var userName = await _userManager.GetUserNameAsync(user);

            var persona = await _context.Persona.Include(x => x.TipoDocumento).Where(x => x.Id == user.PersonaId).FirstOrDefaultAsync();


            Username = userName;

            PersonaInput = new ModelInput
            {
                Nombres = persona.Nombres,
                Apellidos = persona.Apellidos,
                TipoDocumentoId = persona.TipoDocumentoId,
                TipoDocumento = persona.TipoDocumento,
                NumeroDocumento = persona.NumeroDocumento,

                Direccion = persona.Direccion,
                Ciudad = persona.Ciudad,
                Telefono = persona.Telefono
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var persona = _context.Persona.Where(x => x.Id == user.PersonaId).FirstOrDefault();
            
            persona.Telefono = PersonaInput.Telefono;
            persona.Direccion = PersonaInput.Direccion;
            persona.Ciudad = PersonaInput.Ciudad;
         
            await _context.SaveChangesAsync();
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
