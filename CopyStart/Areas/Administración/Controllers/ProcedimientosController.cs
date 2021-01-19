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
    public class ProcedimientosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProcedimientosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administración/Procedimientos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Procedimiento.ToListAsync());
        }

        // GET: Administración/Procedimientos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimiento = await _context.Procedimiento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (procedimiento == null)
            {
                return NotFound();
            }

            return View(procedimiento);
        }

        // GET: Administración/Procedimientos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administración/Procedimientos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Codigo,Estado")] Procedimiento procedimiento)
        {
            if (ModelState.IsValid)
            {
                procedimiento.Id = Guid.NewGuid();
                _context.Add(procedimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(procedimiento);
        }

        // GET: Administración/Procedimientos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimiento = await _context.Procedimiento.FindAsync(id);
            if (procedimiento == null)
            {
                return NotFound();
            }
            return View(procedimiento);
        }

        // POST: Administración/Procedimientos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Descripcion,Codigo,Estado")] Procedimiento procedimiento)
        {
            if (id != procedimiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(procedimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcedimientoExists(procedimiento.Id))
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
            return View(procedimiento);
        }

        // GET: Administración/Procedimientos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimiento = await _context.Procedimiento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (procedimiento == null)
            {
                return NotFound();
            }

            return View(procedimiento);
        }

        // POST: Administración/Procedimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var procedimiento = await _context.Procedimiento.FindAsync(id);
            _context.Procedimiento.Remove(procedimiento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcedimientoExists(Guid id)
        {
            return _context.Procedimiento.Any(e => e.Id == id);
        }
    }
}
