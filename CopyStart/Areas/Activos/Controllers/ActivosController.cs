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

namespace CopyStart.Areas.Activos.Controllers
{
    [Authorize]
    [Area("Activos")]
    public class ActivosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Activos/Activos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Activo.Include(a => a.Persona).Include(a => a.TipoActivo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Activos/Activos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activo = await _context.Activo
                .Include(a => a.Persona)
                .Include(a => a.TipoActivo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activo == null)
            {
                return NotFound();
            }

            return View(activo);
        }

        // GET: Activos/Activos/Create
        public IActionResult Create()
        {
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id");
            ViewData["TipoActivoId"] = new SelectList(_context.Set<TipoActivo>(), "Id", "Codigo");
            return View();
        }

        // POST: Activos/Activos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Serial,TipoActivoId,Descripcion,Marca,Modelo,Ubicacion,FechaRegistro,PersonaId")] Activo activo)
        {
            if (ModelState.IsValid)
            {
                activo.Id = Guid.NewGuid();
                _context.Add(activo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", activo.PersonaId);
            ViewData["TipoActivoId"] = new SelectList(_context.Set<TipoActivo>(), "Id", "Codigo", activo.TipoActivoId);
            return View(activo);
        }

        // GET: Activos/Activos/Edit/5
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
            return View(activo);
        }

        // POST: Activos/Activos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Serial,TipoActivoId,Descripcion,Marca,Modelo,Ubicacion,FechaRegistro,PersonaId")] Activo activo)
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
            return View(activo);
        }

        // GET: Activos/Activos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activo = await _context.Activo
                .Include(a => a.Persona)
                .Include(a => a.TipoActivo)
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
    }
}
