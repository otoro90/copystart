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
    public class DiagnosticosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiagnosticosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tecnica/Diagnosticoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Diagnostico.Include(d => d.Soportes).Include(d => d.TipoServicio);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tecnica/Diagnosticoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnostico
                .Include(d => d.Soportes)
                .Include(d => d.TipoServicio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // GET: Tecnica/Diagnosticoes/Create
        public IActionResult Create()
        {
            ViewData["SoportesId"] = new SelectList(_context.Soporte, "Id", "Id");
            ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Codigo");
            return View();
        }

        // POST: Tecnica/Diagnosticoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripción,FechaDiagnostico,TipoServicioId,SoportesId")] Diagnostico diagnostico)
        {
            if (ModelState.IsValid)
            {
                diagnostico.Id = Guid.NewGuid();
                _context.Add(diagnostico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SoportesId"] = new SelectList(_context.Soporte, "Id", "Id", diagnostico.SoportesId);
            ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Codigo", diagnostico.TipoServicioId);
            return View(diagnostico);
        }

        // GET: Tecnica/Diagnosticoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnostico.FindAsync(id);
            if (diagnostico == null)
            {
                return NotFound();
            }
            ViewData["SoportesId"] = new SelectList(_context.Soporte, "Id", "Id", diagnostico.SoportesId);
            ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Codigo", diagnostico.TipoServicioId);
            return View(diagnostico);
        }

        // POST: Tecnica/Diagnosticoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Descripción,FechaDiagnostico,TipoServicioId,SoportesId")] Diagnostico diagnostico)
        {
            if (id != diagnostico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagnostico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosticoExists(diagnostico.Id))
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
            ViewData["SoportesId"] = new SelectList(_context.Soporte, "Id", "Id", diagnostico.SoportesId);
            ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Codigo", diagnostico.TipoServicioId);
            return View(diagnostico);
        }

        // GET: Tecnica/Diagnosticoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnostico
                .Include(d => d.Soportes)
                .Include(d => d.TipoServicio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // POST: Tecnica/Diagnosticoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var diagnostico = await _context.Diagnostico.FindAsync(id);
            _context.Diagnostico.Remove(diagnostico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosticoExists(Guid id)
        {
            return _context.Diagnostico.Any(e => e.Id == id);
        }
    }
}
