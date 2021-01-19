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

namespace CopyStart.Areas.Administración.Controllers
{
    [Area("Administración")]
    [Authorize]
    public class ProcedimientoTipoServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProcedimientoTipoServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administración/ProcedimientoTipoServicios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProcedimientoTipoServicio.Include(p => p.Procedimientos).Include(p => p.TipoServicio);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administración/ProcedimientoTipoServicios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimientoTipoServicio = await _context.ProcedimientoTipoServicio
                .Include(p => p.Procedimientos)
                .Include(p => p.TipoServicio)
                .FirstOrDefaultAsync(m => m.ProcedimientoId == id);
            if (procedimientoTipoServicio == null)
            {
                return NotFound();
            }

            return View(procedimientoTipoServicio);
        }

        // GET: Administración/ProcedimientoTipoServicios/Create
        public IActionResult Create()
        {
            ViewData["ProcedimientoId"] = new SelectList(_context.Procedimiento, "Id", "Codigo");
            ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Codigo");
            return View();
        }

        // POST: Administración/ProcedimientoTipoServicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProcedimientoId,Numero,TipoServicioId")] ProcedimientoTipoServicio procedimientoTipoServicio)
        {
            if (ModelState.IsValid)
            {
                procedimientoTipoServicio.ProcedimientoId = Guid.NewGuid();
                _context.Add(procedimientoTipoServicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProcedimientoId"] = new SelectList(_context.Procedimiento, "Id", "Codigo", procedimientoTipoServicio.ProcedimientoId);
            ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Codigo", procedimientoTipoServicio.TipoServicioId);
            return View(procedimientoTipoServicio);
        }

        // GET: Administración/ProcedimientoTipoServicios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimientoTipoServicio = await _context.ProcedimientoTipoServicio.FindAsync(id);
            if (procedimientoTipoServicio == null)
            {
                return NotFound();
            }
            ViewData["ProcedimientoId"] = new SelectList(_context.Procedimiento, "Id", "Codigo", procedimientoTipoServicio.ProcedimientoId);
            ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Codigo", procedimientoTipoServicio.TipoServicioId);
            return View(procedimientoTipoServicio);
        }

        // POST: Administración/ProcedimientoTipoServicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProcedimientoId,Numero,TipoServicioId")] ProcedimientoTipoServicio procedimientoTipoServicio)
        {
            if (id != procedimientoTipoServicio.ProcedimientoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(procedimientoTipoServicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcedimientoTipoServicioExists(procedimientoTipoServicio.ProcedimientoId))
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
            ViewData["ProcedimientoId"] = new SelectList(_context.Procedimiento, "Id", "Codigo", procedimientoTipoServicio.ProcedimientoId);
            ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Codigo", procedimientoTipoServicio.TipoServicioId);
            return View(procedimientoTipoServicio);
        }

        // GET: Administración/ProcedimientoTipoServicios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimientoTipoServicio = await _context.ProcedimientoTipoServicio
                .Include(p => p.Procedimientos)
                .Include(p => p.TipoServicio)
                .FirstOrDefaultAsync(m => m.ProcedimientoId == id);
            if (procedimientoTipoServicio == null)
            {
                return NotFound();
            }

            return View(procedimientoTipoServicio);
        }

        // POST: Administración/ProcedimientoTipoServicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var procedimientoTipoServicio = await _context.ProcedimientoTipoServicio.FindAsync(id);
            _context.ProcedimientoTipoServicio.Remove(procedimientoTipoServicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcedimientoTipoServicioExists(Guid id)
        {
            return _context.ProcedimientoTipoServicio.Any(e => e.ProcedimientoId == id);
        }
    }
}
