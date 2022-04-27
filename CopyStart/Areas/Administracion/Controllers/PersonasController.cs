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

namespace CopyStart.Areas.Administracion.Controllers
{
    [Area("Administracion")]
    [Authorize(Roles = "Administrador ")]
    public class PersonasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administracion/Personas

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Persona.Include(p => p.TipoDocumento);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administracion/Personas/Details/5
        [Authorize(Roles = "Coordinador")]
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

            return View(persona);
        }

        // GET: Administracion/Personas/Create
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


        public IActionResult CompleteData()
        {
            ViewData["TipoDocumentoId"] = new SelectList(_context.TipoDocumento, "Id", "Codigo");
            return View();
        }

        // POST: Administracion/Personas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteData([Bind("Id,Nombres,Apellidos,TipoDocumentoId,NumeroDocumento,Direccion,Ciudad,Telefono,Estado")] Persona persona)
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

        [Authorize(Roles = "Coordinador")]
        public async Task<IActionResult> ListadoPersonalTecnico()
        {
            var applicationDbContext = _context.Persona.Include(p => p.TipoDocumento);
            return View(await applicationDbContext.ToListAsync());
        }
    }

}
