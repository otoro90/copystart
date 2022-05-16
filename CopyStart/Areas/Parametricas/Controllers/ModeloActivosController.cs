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
    public class ModeloActivosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModeloActivosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parametricas/ModeloActivos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ModeloActivo.Include(m => m.MarcaActivo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Parametricas/ModeloActivos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloActivo = await _context.ModeloActivo
                .Include(m => m.MarcaActivo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modeloActivo == null)
            {
                return NotFound();
            }

            return View(modeloActivo);
        }

        // GET: Parametricas/ModeloActivos/Create
        public IActionResult Create()
        {
            ViewData["MarcaActivoId"] = new SelectList(_context.MarcaActvo, "Id", "Codigo");
            return View();
        }

        // POST: Parametricas/ModeloActivos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MarcaActivoId,Id,Nombre,Descripcion,Codigo,Estado")] ModeloActivo modeloActivo)
        {
            if (ModelState.IsValid)
            {
                modeloActivo.Id = Guid.NewGuid();
                _context.Add(modeloActivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarcaActivoId"] = new SelectList(_context.MarcaActvo, "Id", "Codigo", modeloActivo.MarcaActivoId);
            return View(modeloActivo);
        }

        // GET: Parametricas/ModeloActivos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloActivo = await _context.ModeloActivo.FindAsync(id);
            if (modeloActivo == null)
            {
                return NotFound();
            }
            ViewData["MarcaActivoId"] = new SelectList(_context.MarcaActvo, "Id", "Codigo", modeloActivo.MarcaActivoId);
            return View(modeloActivo);
        }

        // POST: Parametricas/ModeloActivos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MarcaActivoId,Id,Nombre,Descripcion,Codigo,Estado")] ModeloActivo modeloActivo)
        {
            if (id != modeloActivo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modeloActivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeloActivoExists(modeloActivo.Id))
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
            ViewData["MarcaActivoId"] = new SelectList(_context.MarcaActvo, "Id", "Codigo", modeloActivo.MarcaActivoId);
            return View(modeloActivo);
        }

        // GET: Parametricas/ModeloActivos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modeloActivo = await _context.ModeloActivo
                .Include(m => m.MarcaActivo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modeloActivo == null)
            {
                return NotFound();
            }

            return View(modeloActivo);
        }

        // POST: Parametricas/ModeloActivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var modeloActivo = await _context.ModeloActivo.FindAsync(id);
            _context.ModeloActivo.Remove(modeloActivo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModeloActivoExists(Guid id)
        {
            return _context.ModeloActivo.Any(e => e.Id == id);
        }
    }
}
