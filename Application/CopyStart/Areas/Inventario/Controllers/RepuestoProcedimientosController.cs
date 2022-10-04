using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CopyStart.Data;
using CopyStart.Entities;
using Microsoft.AspNetCore.Authorization;

namespace CopyStart.Areas.Inventario.Controllers
{
    [Area("Inventario")]
    [Authorize]
    public class RepuestoProcedimientosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RepuestoProcedimientosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Inventario/RepuestoProcedimientos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RepuestoProcedimiento.Include(r => r.Procedimientos).Include(r => r.Repuesto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Inventario/RepuestoProcedimientos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuestoProcedimiento = await _context.RepuestoProcedimiento
                .Include(r => r.Procedimientos)
                .Include(r => r.Repuesto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repuestoProcedimiento == null)
            {
                return NotFound();
            }

            return View(repuestoProcedimiento);
        }

        // GET: Inventario/RepuestoProcedimientos/Create
        public IActionResult Create()
        {
            ViewData["ProcedimientoId"] = new SelectList(_context.Procedimiento, "Id", "Codigo");
            ViewData["RepuestoId"] = new SelectList(_context.Repuesto, "Id", "Codigo");
            return View();
        }

        // POST: Inventario/RepuestoProcedimientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProcedimientoId,RepuestoId,Cantidad")] RepuestoProcedimiento repuestoProcedimiento)
        {
            if (ModelState.IsValid)
            {
                repuestoProcedimiento.Id = Guid.NewGuid();
                _context.Add(repuestoProcedimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProcedimientoId"] = new SelectList(_context.Procedimiento, "Id", "Codigo", repuestoProcedimiento.ProcedimientoId);
            ViewData["RepuestoId"] = new SelectList(_context.Repuesto, "Id", "Codigo", repuestoProcedimiento.RepuestoId);
            return View(repuestoProcedimiento);
        }

        // GET: Inventario/RepuestoProcedimientos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuestoProcedimiento = await _context.RepuestoProcedimiento.FindAsync(id);
            if (repuestoProcedimiento == null)
            {
                return NotFound();
            }
            ViewData["ProcedimientoId"] = new SelectList(_context.Procedimiento, "Id", "Codigo", repuestoProcedimiento.ProcedimientoId);
            ViewData["RepuestoId"] = new SelectList(_context.Repuesto, "Id", "Codigo", repuestoProcedimiento.RepuestoId);
            return View(repuestoProcedimiento);
        }

        // POST: Inventario/RepuestoProcedimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProcedimientoId,RepuestoId,Cantidad")] RepuestoProcedimiento repuestoProcedimiento)
        {
            if (id != repuestoProcedimiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repuestoProcedimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepuestoProcedimientoExists(repuestoProcedimiento.Id))
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
            ViewData["ProcedimientoId"] = new SelectList(_context.Procedimiento, "Id", "Codigo", repuestoProcedimiento.ProcedimientoId);
            ViewData["RepuestoId"] = new SelectList(_context.Repuesto, "Id", "Codigo", repuestoProcedimiento.RepuestoId);
            return View(repuestoProcedimiento);
        }

        // GET: Inventario/RepuestoProcedimientos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuestoProcedimiento = await _context.RepuestoProcedimiento
                .Include(r => r.Procedimientos)
                .Include(r => r.Repuesto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repuestoProcedimiento == null)
            {
                return NotFound();
            }

            return View(repuestoProcedimiento);
        }

        // POST: Inventario/RepuestoProcedimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var repuestoProcedimiento = await _context.RepuestoProcedimiento.FindAsync(id);
            _context.RepuestoProcedimiento.Remove(repuestoProcedimiento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepuestoProcedimientoExists(Guid id)
        {
            return _context.RepuestoProcedimiento.Any(e => e.Id == id);
        }
    }
}
