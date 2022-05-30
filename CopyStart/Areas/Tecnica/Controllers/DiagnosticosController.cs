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
using CopyStart.Areas.Tecnica.Models;

namespace CopyStart.Areas.Tecnica.Controllers
{
   
    [Area("Tecnica")]
    public class DiagnosticosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiagnosticosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tecnica/Diagnosticos
        [Authorize(Roles = "Administrador, Coordinador")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Diagnostico.Include(d => d.Soportes);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tecnica/Diagnosticos/Details/5
        [Authorize(Roles = "Administrador, Coordinador, Tecnico, Cliente")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnostico
                .Include(d => d.Soportes)               
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // GET: Tecnica/Diagnosticos/Create
        [Authorize(Roles = "Administrador, Tecnico")]
        public IActionResult Create(string idSolicitud)
        {
            ViewData["SoportesId"] = new SelectList(_context.Soporte, "Id", "Id");
            ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Codigo");
            ViewData["SolicitudId"] = idSolicitud;
            return View();
        }

        // POST: Tecnica/Diagnosticos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Tecnico")]
        public async Task<IActionResult> Create([Bind("Id,Descripción,FechaDiagnostico,TipoServicioId,SoportesId,SolicitudId")] CreateDiagnosticosModel diagnostico)
        {
            diagnostico.FechaDiagnostico = DateTime.Now;
            if (ModelState.IsValid)
            {


               
                _context.Add(diagnostico);
                var servicio = new Servicio();
               

                servicio.SolicitudId = (long)diagnostico.SolicitudId;

                var solicitud = await _context.Solicitud
                .Include(s => s.Activo)
                .Include(s => s.Cliente)
                .Include(s => s.Tecnico)
                .FirstOrDefaultAsync(m => m.Id == servicio.SolicitudId);

                servicio.TipoServicioId = diagnostico.TipoServicioId;               
                servicio.DiagnosticoId = diagnostico.Id;
                servicio.ActivoId = solicitud.ActivoId;              
                servicio.FechaRealizacion = DateTime.MinValue;
                servicio.Estado = "Por confirmar";
                solicitud.EstadoSolicitud="Diagnosticada";
                _context.Add(servicio);
                _context.Update(solicitud);
                
               

                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Diagnosticos", new { area = "Tecnica", id = diagnostico.Id });
            }
            ViewData["SoportesId"] = new SelectList(_context.Soporte, "Id", "Id", diagnostico.SoportesId);
            ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Codigo", diagnostico.TipoServicioId);
            return View(diagnostico);
        }

        // GET: Tecnica/Diagnosticos/Edit/5
        [Authorize(Roles = "Administrador")]
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
            
            return View(diagnostico);
        }

        // POST: Tecnica/Diagnosticos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Descripción,FechaDiagnostico,SoportesId")] Diagnostico diagnostico)
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
            
            return View(diagnostico);
        }

        // GET: Tecnica/Diagnosticos/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnostico
                .Include(d => d.Soportes)               
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // POST: Tecnica/Diagnosticos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var diagnostico = await _context.Diagnostico.FindAsync(id);
            _context.Diagnostico.Remove(diagnostico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosticoExists(long id)
        {
            return _context.Diagnostico.Any(e => e.Id == id);
        }
    }
}
