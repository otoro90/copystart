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

namespace CopyStart.Areas.Activos.Controllers
{
    [Area("Activos")]
    
  
    public class ActivosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ActivosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Activos/Activos
        [Authorize(Roles = "Administrador, Coordinador")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Activo.Include(a => a.Persona).Include(a => a.TipoActivo).Include(a => a.MarcaActivo).Include(a => a.ModeloActivo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Activos/Activos/Details/5
        [Authorize(Roles = "Administrador, Coordinador, Tecnico, Cliente")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activo = await _context.Activo
                .Include(a => a.Persona)
                .Include(a => a.TipoActivo)
                .Include(a => a.MarcaActivo)
                .Include(a => a.ModeloActivo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activo == null)
            {
                return NotFound();
            }

            return View(activo);
        }

        // GET: Activos/Activos/Create
        [Authorize(Roles = "Administrador, Cliente")]
        public IActionResult Create()
        {
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id");
            ViewData["TipoActivoId"] = new SelectList(_context.Set<TipoActivo>(), "Id", "Nombre");
            ViewData["MarcaActivoId"] = new SelectList(_context.Set<MarcaActivo>(), "Id", "Nombre");
            ViewData["ModeloActivoId"] = new SelectList(_context.Set<ModeloActivo>(), "Id", "Nombre");
            return View();
        }

        // POST: Activos/Activos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Create([Bind("Id,Serial,TipoActivoId,Descripcion,MarcaActivoId,ModeloActivoId,FechaRegistro,PersonaId")] Activo activo)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                activo.Id = Guid.NewGuid();
               
                activo.FechaRegistro = DateTime.Now;
                activo.PersonaId = (Guid)user.PersonaId;
                _context.Add(activo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Activos", new { area = "Activos", id = activo.Id });
            }

            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", activo.PersonaId);
            ViewData["TipoActivoId"] = new SelectList(_context.Set<TipoActivo>(), "Id", "Codigo", activo.TipoActivoId);
            ViewData["MarcaActivoId"] = new SelectList(_context.Set<MarcaActivo>(), "Id", "Nombre", activo.MarcaActivoId);
            ViewData["ModeloActivoId"] = new SelectList(_context.Set<ModeloActivo>(), "Id", "Nombre", activo.ModeloActivoId);
            return View(activo);
        }

        // GET: Activos/Activos/Edit/5
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activo = await _context.Activo.FindAsync(id);
            if (activo == null)
            {
                return NotFound();
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", activo.PersonaId);
            ViewData["TipoActivoId"] = new SelectList(_context.Set<TipoActivo>(), "Id", "Codigo", activo.TipoActivoId);
            ViewData["MarcaActivoId"] = new SelectList(_context.Set<MarcaActivo>(), "Id", "Nombre", activo.MarcaActivoId);
            ViewData["ModeloActivoId"] = new SelectList(_context.Set<ModeloActivo>(), "Id", "Nombre", activo.ModeloActivoId);
            ViewData["Serial"] = activo.Serial;
            ViewData["Estado"] = activo.Estado;

            return View(activo);
        }

        // POST: Activos/Activos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Serial,TipoActivoId,Descripcion,MarcaActivoId,ModeloActivoId,FechaRegistro,PersonaId,Estado")] Activo activo)
        {
            if (id != activo.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivoExists(activo.Id))
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
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", activo.PersonaId);
            ViewData["TipoActivoId"] = new SelectList(_context.Set<TipoActivo>(), "Id", "Codigo", activo.TipoActivoId);
            ViewData["MarcaActivoId"] = new SelectList(_context.Set<MarcaActivo>(), "Id", "Nombre", activo.MarcaActivoId);
            ViewData["ModeloActivoId"] = new SelectList(_context.Set<ModeloActivo>(), "Id", "Nombre", activo.ModeloActivoId);
            return View(activo);
        }

        // GET: Activos/Activos/Delete/5
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activo = await _context.Activo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activo == null)
            {
                return NotFound();
            }

            return View(activo);
        }

        // POST: Activos/Activos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var activo = await _context.Activo.FindAsync(id);
            _context.Activo.Remove(activo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivoExists(Guid id)
        {
            return _context.Activo.Any(e => e.Id == id);
        }


        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> ActivosPropios()
        {
            var user = await _userManager.GetUserAsync(User);
            var applicationDbContext = _context.Activo.Include(a => a.Persona).Include(a => a.TipoActivo).Where(x=>x.PersonaId==user.PersonaId);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ActivosPorCliente(Guid? idCliente)
        {
           
            var applicationDbContext = _context.Activo.Include(a => a.Persona).Include(a => a.TipoActivo).Where(x => x.PersonaId == idCliente);
            return View(await applicationDbContext.ToListAsync());
        }




    }
}
