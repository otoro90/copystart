using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CopyStart.Data;
using CopyStart.Entities;

namespace CopyStart.Areas.Administracion.Controllers
{
    [Area("Administracion")]
    public class ProcedimientoTipoServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProcedimientoTipoServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administracion/ProcedimientoTipoServicios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProcedimientoTipoServicio.Include(p => p.Procedimientos).Include(p => p.TipoServicio);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administracion/ProcedimientoTipoServicios/Details/5
        public async Task<IActionResult> Details(Guid? id, Guid? id2)
        {
            if ((id == null))
            {
                return NotFound();
            }

            var procedimientoTipoServicio = await _context.ProcedimientoTipoServicio
                .Include(p => p.Procedimientos)
                .Include(p => p.TipoServicio)
                .FirstOrDefaultAsync(m => m.TipoServicioId == id);
            if (procedimientoTipoServicio == null)
            {
                return NotFound();
            }

            return View(procedimientoTipoServicio);
        }

        // GET: Administracion/ProcedimientoTipoServicios/Create
        public IActionResult Create()
        {
            ViewData["ProcedimientoId"] = new SelectList(_context.Procedimiento, "Id", "Codigo");
            ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Codigo");
            return View();
        }

        // POST: Administracion/ProcedimientoTipoServicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Numero,ProcedimientoId,TipoServicioId")] ProcedimientoTipoServicio procedimientoTipoServicio)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(procedimientoTipoServicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProcedimientoId"] = new SelectList(_context.Procedimiento, "Id", "Codigo", procedimientoTipoServicio.ProcedimientoId);
            ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Codigo", procedimientoTipoServicio.TipoServicioId);
            return View(procedimientoTipoServicio);
        }

        // GET: Administracion/ProcedimientoTipoServicios/Edit/5
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

        // POST: Administracion/ProcedimientoTipoServicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Numero,ProcedimientoId,TipoServicioId")] ProcedimientoTipoServicio procedimientoTipoServicio)
        {
            if (id != procedimientoTipoServicio.TipoServicioId)
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
                    if (!ProcedimientoTipoServicioExists(procedimientoTipoServicio.TipoServicioId))
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

        // GET: Administracion/ProcedimientoTipoServicios/Delete/5
        [Route("Delete/{id1}/{id2}")]
        public async Task<IActionResult> Delete(Guid? id1, Guid? id2)
        {
            if (id1 == null)
            {
                return NotFound();
            }

            var procedimientoTipoServicio = await _context.ProcedimientoTipoServicio
                .Include(p => p.Procedimientos)
                .Include(p => p.TipoServicio)
                .FirstOrDefaultAsync(m => m.TipoServicioId == id1);
            if (procedimientoTipoServicio == null)
            {
                return NotFound();
            }

            return View(procedimientoTipoServicio);
        }

        // POST: Administracion/ProcedimientoTipoServicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, Guid? id2)
        {
            var procedimientoTipoServicio = await _context.ProcedimientoTipoServicio.FindAsync(id, id2);
            _context.ProcedimientoTipoServicio.Remove(procedimientoTipoServicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcedimientoTipoServicioExists(Guid id)
        {
            return _context.ProcedimientoTipoServicio.Any(e => e.TipoServicioId == id);
        }
    }
}
