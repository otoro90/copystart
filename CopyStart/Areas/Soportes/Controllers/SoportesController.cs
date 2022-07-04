using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CopyStart.Data;
using CopyStart.Entities;
using Microsoft.AspNetCore.Authorization;

namespace CopyStart.Areas.Soportes.Controllers
{
    [Authorize]
    [Area("Soportes")]
    public class SoportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SoportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Soportes/Soportes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Soporte.Include(s => s.DocumentoCertificacion);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Soportes/Soportes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soporte = await _context.Soporte
                .Include(s => s.DocumentoCertificacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soporte == null)
            {
                return NotFound();
            }

            return View(soporte);
        }

        // GET: Soportes/Soportes/Create
        public IActionResult Create()
        {
            ViewData["DocumentoCertificacionId"] = new SelectList(_context.Certificacion, "Id", "Nombre");
            return View();
        }

        // POST: Soportes/Soportes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DocumentoReciboId,CopiaFacturaFirmadaId,DocumentoCertificacionId")] Soporte soporte)
        {
            if (ModelState.IsValid)
            {
                soporte.Id = Guid.NewGuid();
                _context.Add(soporte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DocumentoCertificacionId"] = new SelectList(_context.Certificacion, "Id", "Nombre", soporte.DocumentoCertificacionId);
            return View(soporte);
        }

        // GET: Soportes/Soportes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soporte = await _context.Soporte.FindAsync(id);
            if (soporte == null)
            {
                return NotFound();
            }
            ViewData["DocumentoCertificacionId"] = new SelectList(_context.Certificacion, "Id", "Nombre", soporte.DocumentoCertificacionId);
            return View(soporte);
        }

        // POST: Soportes/Soportes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DocumentoReciboId,CopiaFacturaFirmadaId,DocumentoCertificacionId")] Soporte soporte)
        {
            if (id != soporte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soporte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoporteExists(soporte.Id))
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
            ViewData["DocumentoCertificacionId"] = new SelectList(_context.Certificacion, "Id", "Nombre", soporte.DocumentoCertificacionId);
            return View(soporte);
        }

        // GET: Soportes/Soportes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soporte = await _context.Soporte
                .Include(s => s.DocumentoCertificacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soporte == null)
            {
                return NotFound();
            }

            return View(soporte);
        }

        // POST: Soportes/Soportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var soporte = await _context.Soporte.FindAsync(id);
            _context.Soporte.Remove(soporte);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoporteExists(Guid id)
        {
            return _context.Soporte.Any(e => e.Id == id);
        }
    }
}
