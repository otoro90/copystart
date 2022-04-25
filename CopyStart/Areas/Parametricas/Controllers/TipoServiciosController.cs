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

namespace CopyStart.Areas.Parametricas.Controllers
{
    [Authorize]
    [Area("Parametricas")]
    public class TipoServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parametricas/TipoServicios
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoServicio.ToListAsync());
        }

        // GET: Parametricas/TipoServicios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServicio = await _context.TipoServicio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoServicio == null)
            {
                return NotFound();
            }

            return View(tipoServicio);
        }

        // GET: Parametricas/TipoServicios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parametricas/TipoServicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Codigo,Estado")] TipoServicio tipoServicio)
        {
            if (ModelState.IsValid)
            {
                tipoServicio.Id = Guid.NewGuid();
                _context.Add(tipoServicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoServicio);
        }

        // GET: Parametricas/TipoServicios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServicio = await _context.TipoServicio.FindAsync(id);
            if (tipoServicio == null)
            {
                return NotFound();
            }
            return View(tipoServicio);
        }

        // POST: Parametricas/TipoServicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Descripcion,Codigo,Estado")] TipoServicio tipoServicio)
        {
            if (id != tipoServicio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoServicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoServicioExists(tipoServicio.Id))
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
            return View(tipoServicio);
        }

        // GET: Parametricas/TipoServicios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoServicio = await _context.TipoServicio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoServicio == null)
            {
                return NotFound();
            }

            return View(tipoServicio);
        }

        // POST: Parametricas/TipoServicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tipoServicio = await _context.TipoServicio.FindAsync(id);
            _context.TipoServicio.Remove(tipoServicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoServicioExists(Guid id)
        {
            return _context.TipoServicio.Any(e => e.Id == id);
        }


        public async Task<IActionResult> VerProcedimientosAsync(Guid idTipoServicio)
        {
            var procedimientos = _context.ProcedimientoTipoServicio.Where(e => e.TipoServicioId == idTipoServicio).Include(s => s.Procedimientos).Include(s => s.TipoServicio);
            return View(await procedimientos.ToListAsync());


        }

    }
}
