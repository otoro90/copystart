using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CopyStart.Data;
using CopyStart.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CopyStart.Areas.Administracion.Models;

namespace CopyStart.Areas.Administracion.Controllers
{
    [Area("Administracion")]
    
    public class PersonasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PersonasController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Administracion/Personas


        [Authorize(Roles = "Administrador, Coordinador")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Persona.Include(p => p.TipoDocumento);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administracion/Personas/Details/5
        [Authorize(Roles = "Administrador, Coordinador, Tecnico")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona
                .Include(p => p.TipoDocumento)
                .FirstOrDefaultAsync(m => m.Id == id);
             


            if (persona == null)
            {
                return NotFound();
            }
            var usuario = await _context.Users.Include(m => m.Persona).FirstOrDefaultAsync(m => m.PersonaId == id);
            var applicationUserRoles = await _context.ApplicationUserRole.Include(m=>m.Role).Where(m => m.UserId == usuario.Id).ToListAsync();

            var detallePersonaVm = new DetallePersonaVM
            {
                Persona = persona,
                RolesUsuario = applicationUserRoles
            };

            return View(detallePersonaVm);
        }

        // GET: Administracion/Personas/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            ViewData["TipoDocumentoId"] = new SelectList(_context.TipoDocumento, "Id", "Codigo");
            return View();
        }

        // POST: Administracion/Personas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("Id,Nombres,Apellidos,TipoDocumentoId,NumeroDocumento,Direccion,Ciudad,Telefono,Estado")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                persona.Id = Guid.NewGuid();
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoDocumentoId"] = new SelectList(_context.TipoDocumento, "Id", "Codigo", persona.TipoDocumentoId);
            return View(persona);
        }

        // GET: Administracion/Personas/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            ViewData["TipoDocumentoId"] = new SelectList(_context.TipoDocumento, "Id", "Codigo", persona.TipoDocumentoId);
            return View(persona);
        }

        // POST: Administracion/Personas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombres,Apellidos,TipoDocumentoId,NumeroDocumento,Direccion,Ciudad,Telefono,Estado")] Persona persona)
        {
            if (id != persona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoDocumentoId"] = new SelectList(_context.TipoDocumento, "Id", "Codigo", persona.TipoDocumentoId);
            return View(persona);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Administracion/Personas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona
                .Include(p => p.TipoDocumento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Administracion/Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var persona = await _context.Persona.FindAsync(id);
            _context.Persona.Remove(persona);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(Guid id)
        {
            return _context.Persona.Any(e => e.Id == id);
        }

        [Authorize]
        public IActionResult CompleteData()
        {
            ViewData["TipoDocumentoId"] = new SelectList(_context.TipoDocumento, "Id", "Codigo");
            ViewData["UbicacionId"] = new SelectList(_context.Ubicacion, "CodigoLugar", "Lugar");
            return View();
        }

        // POST: Administracion/Personas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CompleteData([Bind("Id,Nombres,Apellidos,TipoDocumentoId,NumeroDocumento,Direccion,UbicacionId,Telefono,Estado")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                persona.Id = Guid.NewGuid();
                
                _context.Add(persona);
                var user = _context.User.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
                user.PersonaId = persona.Id;
                
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Activos", new { area = "Activos" });
            }
            ViewData["TipoDocumentoId"] = new SelectList(_context.TipoDocumento, "Id", "Codigo", persona.TipoDocumentoId);
            return View(persona);
        }

        [Authorize(Roles = "Administrador, Coordinador")]
        public async Task<IActionResult> ListadoTecnicos()
        {
            var applicationDbContext = _context.UserRoles.Where(x => x.RoleId == "TEC").Include(a => a.User).ThenInclude(a=>a.Persona);
            return View(await applicationDbContext.ToListAsync());
        }


        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AsignarRol(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona.FindAsync(id);
            var usuario = await _context.Users
               .Include(s => s.Persona)
               .FirstOrDefaultAsync(m => m.PersonaId == id);

            

            if (persona == null)
            {
                return NotFound();
            }

            ViewData["UserId"] = new SelectList(_context.Persona.Select(x => new { Id = x.Id, Texto = x.Nombres + " " + x.Apellidos + " - " + x.NumeroDocumento }), "Id", "Texto", id);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AsignarRol(Guid? id, [Bind("RoleId")] ApplicationUserRole rol)
        {
            var usuario = await _context.User
                .FirstOrDefaultAsync(m => m.PersonaId == id);

            var applicationUserRole = await _context.ApplicationUserRole
                .FirstOrDefaultAsync(m => m.UserId == usuario.Id && m.RoleId == rol.RoleId);

          

            if (ModelState.IsValid )
            {
                if (applicationUserRole != null)
                {
                    throw new ArgumentException("El usuario ya tiene este rol");
                }

                var input = new ApplicationUserRole();
                input.RoleId = rol.RoleId;
                input.UserId = usuario.Id;
                _context.Add(input);
            }


            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Personas", new { area = "Administracion" });

        }


        [Authorize(Roles = "Administrador")]
      
        public async Task<IActionResult> BorrarRol(string id, Guid? persona)
        {
            
            

            var applicationUserRole = await _context.ApplicationUserRole
                .FirstOrDefaultAsync(m => m.RoleId == id);
            _context.ApplicationUserRole.Remove(applicationUserRole);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Personas", new { area = "Administracion", id = persona });
        }

        
    }


}
