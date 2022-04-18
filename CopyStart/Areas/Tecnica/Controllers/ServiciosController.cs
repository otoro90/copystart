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
    public class ServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tecnica/Servicios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Servicio.Include(s => s.Activo).Include(s => s.Diagnostico.TipoServicio).Include(s => s.Solicitudes.Cliente).Include(s => s.Soportes);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tecnica/Servicios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio
                .Include(s => s.Activo)
                //.Include(s => s.Diagnostico.TipoServicio)
                .Include(s => s.Diagnostico).ThenInclude( x=> x.TipoServicio)
                .Include(s => s.Solicitudes)
                .Include(s => s.Soportes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // GET: Tecnica/Servicios/Create
        public IActionResult Create()
        {
            ViewData["ActivoId"] = new SelectList(_context.Activo, "Id", "Marca");
            ViewData["DiagnosticoId"] = new SelectList(_context.Diagnostico, "Id", "Descripción");
            ViewData["SolicitudId"] = new SelectList(_context.Solicitud, "Id", "Descripcion");
            ViewData["SoportesId"] = new SelectList(_context.Soporte, "Id", "Id");
            return View();
        }

        // POST: Tecnica/Servicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Estado,FechaRealizacion,DiagnosticoId,ActivoId,SolicitudId,SoportesId")] Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                servicio.Id = Guid.NewGuid();

                _context.Add(servicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivoId"] = new SelectList(_context.Activo, "Id", "Marca", servicio.ActivoId);
            ViewData["DiagnosticoId"] = new SelectList(_context.Diagnostico, "Id", "Descripción", servicio.DiagnosticoId);
            ViewData["SolicitudId"] = new SelectList(_context.Solicitud, "Id", "Descripcion", servicio.SolicitudId);
            ViewData["SoportesId"] = new SelectList(_context.Soporte, "Id", "Id", servicio.SoportesId);
            return View(servicio);
        }

        // GET: Tecnica/Servicios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }
            ViewData["ActivoId"] = new SelectList(_context.Activo, "Id", "Marca", servicio.ActivoId);
            ViewData["DiagnosticoId"] = new SelectList(_context.Diagnostico, "Id", "Descripción", servicio.DiagnosticoId);
            ViewData["SolicitudId"] = new SelectList(_context.Solicitud, "Id", "Descripcion", servicio.SolicitudId);
            ViewData["SoportesId"] = new SelectList(_context.Soporte, "Id", "Id", servicio.SoportesId);
            return View(servicio);
        }

        // POST: Tecnica/Servicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Estado,FechaRealizacion,DiagnosticoId,ActivoId,SolicitudId,SoportesId")] Servicio servicio)
        {
            if (id != servicio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioExists(servicio.Id))
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
            ViewData["ActivoId"] = new SelectList(_context.Activo, "Id", "Marca", servicio.ActivoId);
            ViewData["DiagnosticoId"] = new SelectList(_context.Diagnostico, "Id", "Descripción", servicio.DiagnosticoId);
            ViewData["SolicitudId"] = new SelectList(_context.Solicitud, "Id", "Descripcion", servicio.SolicitudId);
            ViewData["SoportesId"] = new SelectList(_context.Soporte, "Id", "Id", servicio.SoportesId);
            return View(servicio);
        }

        // GET: Tecnica/Servicios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio
                .Include(s => s.Activo)
                .Include(s => s.Diagnostico)
                .Include(s => s.Solicitudes)
                .Include(s => s.Soportes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // POST: Tecnica/Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var servicio = await _context.Servicio.FindAsync(id);
            _context.Servicio.Remove(servicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioExists(Guid id)
        {
            return _context.Servicio.Any(e => e.Id == id);
        }
    }
}
