using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CopyStart.Data;
using CopyStart.Entities;
using CopyStart.Filters;

namespace CopyStart.Areas.Tecnica.Controllers
{
    [Area("Tecnica")]
    public class ServicioProcedimientoTipoServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicioProcedimientoTipoServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tecnica/ServicioProcedimientoTipoServicios
        [UrlScriptActionFilter]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ServicioProcedimientoTipoServicio.Include(s => s.ProcedimientoTipoServicio).Include(s => s.Servicio);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tecnica/ServicioProcedimientoTipoServicios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicioProcedimientoTipoServicio = await _context.ServicioProcedimientoTipoServicio
                .Include(s => s.ProcedimientoTipoServicio)
                .Include(s => s.Servicio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicioProcedimientoTipoServicio == null)
            {
                return NotFound();
            }

            return View(servicioProcedimientoTipoServicio);
        }

        // GET: Tecnica/ServicioProcedimientoTipoServicios/Create
        public IActionResult Create()
        {
            ViewData["ProcedimientoTipoServicioId"] = new SelectList(_context.ProcedimientoTipoServicio, "Id", "Id");
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Id", "Id");
            return View();
        }

        // POST: Tecnica/ServicioProcedimientoTipoServicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ServicioId,ProcedimientoTipoServicioId,ProcedimientosRealizados,Observaciones")] ServicioProcedimientoTipoServicio servicioProcedimientoTipoServicio)
        {
            if (ModelState.IsValid)
            {
                servicioProcedimientoTipoServicio.Id = Guid.NewGuid();
                _context.Add(servicioProcedimientoTipoServicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProcedimientoTipoServicioId"] = new SelectList(_context.ProcedimientoTipoServicio, "Id", "Id", servicioProcedimientoTipoServicio.ProcedimientoTipoServicioId);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Id", "Id", servicioProcedimientoTipoServicio.ServicioId);
            return View(servicioProcedimientoTipoServicio);
        }

        // GET: Tecnica/ServicioProcedimientoTipoServicios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicioProcedimientoTipoServicio = await _context.ServicioProcedimientoTipoServicio.FindAsync(id);
            if (servicioProcedimientoTipoServicio == null)
            {
                return NotFound();
            }
            ViewData["ProcedimientoTipoServicioId"] = new SelectList(_context.ProcedimientoTipoServicio, "Id", "Id", servicioProcedimientoTipoServicio.ProcedimientoTipoServicioId);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Id", "Id", servicioProcedimientoTipoServicio.ServicioId);
            return View(servicioProcedimientoTipoServicio);
        }

        // POST: Tecnica/ServicioProcedimientoTipoServicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ServicioId,ProcedimientoTipoServicioId,ProcedimientosRealizados,Observaciones")] ServicioProcedimientoTipoServicio servicioProcedimientoTipoServicio)
        {
            if (id != servicioProcedimientoTipoServicio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicioProcedimientoTipoServicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioProcedimientoTipoServicioExists(servicioProcedimientoTipoServicio.Id))
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
            ViewData["ProcedimientoTipoServicioId"] = new SelectList(_context.ProcedimientoTipoServicio, "Id", "Id", servicioProcedimientoTipoServicio.ProcedimientoTipoServicioId);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Id", "Id", servicioProcedimientoTipoServicio.ServicioId);
            return View(servicioProcedimientoTipoServicio);
        }

        // GET: Tecnica/ServicioProcedimientoTipoServicios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicioProcedimientoTipoServicio = await _context.ServicioProcedimientoTipoServicio
                .Include(s => s.ProcedimientoTipoServicio)
                .Include(s => s.Servicio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicioProcedimientoTipoServicio == null)
            {
                return NotFound();
            }

            return View(servicioProcedimientoTipoServicio);
        }

        // POST: Tecnica/ServicioProcedimientoTipoServicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var servicioProcedimientoTipoServicio = await _context.ServicioProcedimientoTipoServicio.FindAsync(id);
            _context.ServicioProcedimientoTipoServicio.Remove(servicioProcedimientoTipoServicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioProcedimientoTipoServicioExists(Guid id)
        {
            return _context.ServicioProcedimientoTipoServicio.Any(e => e.Id == id);
        }
    }
}
