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

namespace CopyStart.Areas.Tecnica.Controllers
{
    [Authorize]
    [Area("Tecnica")]
    public class ServicioProcedimientosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicioProcedimientosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tecnica/ServicioProcedimientos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ServicioProcedimiento.Include(s => s.Procedimiento).Include(s => s.Servicio);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tecnica/ServicioProcedimientos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicioProcedimiento = await _context.ServicioProcedimiento
                .Include(s => s.Procedimiento)
                .Include(s => s.Servicio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicioProcedimiento == null)
            {
                return NotFound();
            }

            return View(servicioProcedimiento);
        }

        // GET: Tecnica/ServicioProcedimientos/Create
        public IActionResult Create()
        {
            ViewData["ProcedimientoId"] = new SelectList(_context.Procedimiento, "Id", "Codigo");
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Id", "Estado");
            return View();
        }

        // POST: Tecnica/ServicioProcedimientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ServicioId,ProcedimientoId,ProcedimientosRealizados")] ServicioProcedimiento servicioProcedimiento)
        {
            if (ModelState.IsValid)
            {
                servicioProcedimiento.Id = Guid.NewGuid();
                _context.Add(servicioProcedimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProcedimientoId"] = new SelectList(_context.Procedimiento, "Id", "Codigo", servicioProcedimiento.ProcedimientoId);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Id", "Estado", servicioProcedimiento.ServicioId);
            return View(servicioProcedimiento);
        }

        // GET: Tecnica/ServicioProcedimientos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicioProcedimiento = await _context.ServicioProcedimiento.FindAsync(id);
            if (servicioProcedimiento == null)
            {
                return NotFound();
            }
            ViewData["ProcedimientoId"] = new SelectList(_context.Procedimiento, "Id", "Codigo", servicioProcedimiento.ProcedimientoId);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Id", "Estado", servicioProcedimiento.ServicioId);
            return View(servicioProcedimiento);
        }

        // POST: Tecnica/ServicioProcedimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ServicioId,ProcedimientoId,ProcedimientosRealizados")] ServicioProcedimiento servicioProcedimiento)
        {
            if (id != servicioProcedimiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicioProcedimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioProcedimientoExists(servicioProcedimiento.Id))
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
            ViewData["ProcedimientoId"] = new SelectList(_context.Procedimiento, "Id", "Codigo", servicioProcedimiento.ProcedimientoId);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Id", "Estado", servicioProcedimiento.ServicioId);
            return View(servicioProcedimiento);
        }

        // GET: Tecnica/ServicioProcedimientos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicioProcedimiento = await _context.ServicioProcedimiento
                .Include(s => s.Procedimiento)
                .Include(s => s.Servicio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicioProcedimiento == null)
            {
                return NotFound();
            }

            return View(servicioProcedimiento);
        }

        // POST: Tecnica/ServicioProcedimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var servicioProcedimiento = await _context.ServicioProcedimiento.FindAsync(id);
            _context.ServicioProcedimiento.Remove(servicioProcedimiento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioProcedimientoExists(Guid id)
        {
            return _context.ServicioProcedimiento.Any(e => e.Id == id);
        }
    }
}
