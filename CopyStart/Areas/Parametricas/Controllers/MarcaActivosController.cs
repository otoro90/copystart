using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CopyStart.Data;
using CopyStart.Entities;

namespace CopyStart.Areas.Parametricas.Controllers
{
    [Area("Parametricas")]
    public class MarcaActivosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MarcaActivosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parametricas/MarcaActivos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MarcaActvo.Include(m => m.TipoActivo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Parametricas/MarcaActivos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcaActivo = await _context.MarcaActvo
                .Include(m => m.TipoActivo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marcaActivo == null)
            {
                return NotFound();
            }

            return View(marcaActivo);
        }

        // GET: Parametricas/MarcaActivos/Create
        public IActionResult Create()
        {
            ViewData["TipoActivoId"] = new SelectList(_context.TipoActivo, "Id", "Nombre");
            return View();
        }

        // POST: Parametricas/MarcaActivos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoActivoId,Id,Nombre,Descripcion,Codigo,Estado")] MarcaActivo marcaActivo)
        {
            if (ModelState.IsValid)
            {
                marcaActivo.Id = Guid.NewGuid();
                _context.Add(marcaActivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoActivoId"] = new SelectList(_context.TipoActivo, "Id", "Codigo", marcaActivo.TipoActivoId);
            return View(marcaActivo);
        }

        // GET: Parametricas/MarcaActivos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcaActivo = await _context.MarcaActvo.FindAsync(id);
            if (marcaActivo == null)
            {
                return NotFound();
            }
            ViewData["TipoActivoId"] = new SelectList(_context.TipoActivo, "Id", "Codigo", marcaActivo.TipoActivoId);
            return View(marcaActivo);
        }

        // POST: Parametricas/MarcaActivos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TipoActivoId,Id,Nombre,Descripcion,Codigo,Estado")] MarcaActivo marcaActivo)
        {
            if (id != marcaActivo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marcaActivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarcaActivoExists(marcaActivo.Id))
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
            ViewData["TipoActivoId"] = new SelectList(_context.TipoActivo, "Id", "Codigo", marcaActivo.TipoActivoId);
            return View(marcaActivo);
        }

        // GET: Parametricas/MarcaActivos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcaActivo = await _context.MarcaActvo
                .Include(m => m.TipoActivo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marcaActivo == null)
            {
                return NotFound();
            }

            return View(marcaActivo);
        }

        // POST: Parametricas/MarcaActivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var marcaActivo = await _context.MarcaActvo.FindAsync(id);
            _context.MarcaActvo.Remove(marcaActivo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarcaActivoExists(Guid id)
        {
            return _context.MarcaActvo.Any(e => e.Id == id);
        }
    }
}
