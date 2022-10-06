using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CopyStart.Data;
using CopyStart.Entities;
using Microsoft.AspNetCore.Authorization;

namespace CopyStart.Areas.Inventario.Controllers
{
    [Area("Inventario")]
    [Authorize]
    public class RepuestosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RepuestosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Inventario/Repuestos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Repuesto.ToListAsync());
        }

        // GET: Inventario/Repuestos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuesto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repuesto == null)
            {
                return NotFound();
            }

            return View(repuesto);
        }

        // GET: Inventario/Repuestos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventario/Repuestos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Marca,Modelo,Id,Nombre,Descripcion,Codigo,Estado")] Repuesto repuesto)
        {
            if (ModelState.IsValid)
            {
                repuesto.Id = Guid.NewGuid();
                _context.Add(repuesto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(repuesto);
        }

        // GET: Inventario/Repuestos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuesto.FindAsync(id);
            if (repuesto == null)
            {
                return NotFound();
            }
            return View(repuesto);
        }

        // POST: Inventario/Repuestos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Marca,Modelo,Id,Nombre,Descripcion,Codigo,Estado")] Repuesto repuesto)
        {
            if (id != repuesto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repuesto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepuestoExists(repuesto.Id))
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
            return View(repuesto);
        }

        // GET: Inventario/Repuestos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuesto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repuesto == null)
            {
                return NotFound();
            }

            return View(repuesto);
        }

        // POST: Inventario/Repuestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var repuesto = await _context.Repuesto.FindAsync(id);
            _context.Repuesto.Remove(repuesto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepuestoExists(Guid id)
        {
            return _context.Repuesto.Any(e => e.Id == id);
        }
    }
}
