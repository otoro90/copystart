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
using CopyStart.Filters;

namespace CopyStart.Areas.Parametricas.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Area("Parametricas")]
    public class TipoDocumentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoDocumentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parametricas/TipoDocumentos
        [UrlScriptActionFilter]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoDocumento.ToListAsync());
        }

        // GET: Parametricas/TipoDocumentos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDocumento = await _context.TipoDocumento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoDocumento == null)
            {
                return NotFound();
            }

            return View(tipoDocumento);
        }

        // GET: Parametricas/TipoDocumentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parametricas/TipoDocumentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Codigo,Estado")] TipoDocumento tipoDocumento)
        {
            if (ModelState.IsValid)
            {
                tipoDocumento.Id = Guid.NewGuid();
                _context.Add(tipoDocumento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDocumento);
        }

        // GET: Parametricas/TipoDocumentos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDocumento = await _context.TipoDocumento.FindAsync(id);
            if (tipoDocumento == null)
            {
                return NotFound();
            }
            return View(tipoDocumento);
        }

        // POST: Parametricas/TipoDocumentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Descripcion,Codigo,Estado")] TipoDocumento tipoDocumento)
        {
            if (id != tipoDocumento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoDocumento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDocumentoExists(tipoDocumento.Id))
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
            return View(tipoDocumento);
        }

        // GET: Parametricas/TipoDocumentos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDocumento = await _context.TipoDocumento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoDocumento == null)
            {
                return NotFound();
            }

            return View(tipoDocumento);
        }

        // POST: Parametricas/TipoDocumentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tipoDocumento = await _context.TipoDocumento.FindAsync(id);
            _context.TipoDocumento.Remove(tipoDocumento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoDocumentoExists(Guid id)
        {
            return _context.TipoDocumento.Any(e => e.Id == id);
        }
    }
}
