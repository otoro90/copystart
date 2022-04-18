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
using Microsoft.AspNetCore.Identity;

namespace CopyStart.Areas.Tecnica.Controllers
{
    [Authorize]
    [Area("Tecnica")]
    [Authorize]
    public class SolicitudesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SolicitudesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tecnica/Solicitudes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Solicitud.Include(s => s.Activo).Include(s => s.Cliente).Include(s => s.Tecnico);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tecnica/Solicitudes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitud
                .Include(s => s.Activo)
                .Include(s => s.Cliente)
                .Include(s => s.Tecnico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // GET: Tecnica/Solicitudes/Create
        public IActionResult Create()
        {
            ViewData["ActivoId"] = new SelectList(_context.Activo, "Id",  "Modelo");
            ViewData["ClienteId"] = new SelectList(_context.Persona, "Id", "Id");
            ViewData["TecnicoId"] = new SelectList(_context.Persona, "Id", "Id");
            return View();
        }

        // POST: Tecnica/Solicitudes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Incidencia,Descripcion,Ubicacion,FechaSolicitud,EstadoSolicitud,TecnicoId,ClienteId,ActivoId")] Solicitud solicitud)
        {
            var user = await _userManager.GetUserAsync(User);
            solicitud.Id = Guid.NewGuid();
            solicitud.ClienteId = (Guid)user.PersonaId;
            solicitud.FechaSolicitud = DateTime.Now;
            solicitud.EstadoSolicitud = "Sin tramitar";
            
            

            if (ModelState.IsValid)
            {
               
                _context.Add(solicitud);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivoId"] = new SelectList(_context.Activo, "Id", "Marca", solicitud.ActivoId);
            ViewData["ClienteId"] = new SelectList(_context.Persona, "Id", "Id", solicitud.ClienteId);
            ViewData["TecnicoId"] = new SelectList(_context.Persona, "Id", "Id", solicitud.TecnicoId);
            return View(solicitud);
        }

        // GET: Tecnica/Solicitudes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitud.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }
            ViewData["ActivoId"] = new SelectList(_context.Activo, "Id", "Marca", solicitud.ActivoId);
            ViewData["ClienteId"] = new SelectList(_context.Persona, "Id", "Id", solicitud.ClienteId);
            ViewData["TecnicoId"] = new SelectList(_context.Persona, "Id", "Id", solicitud.TecnicoId);
            return View(solicitud);
        }

        // POST: Tecnica/Solicitudes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Incidencia,Descripcion,Ubicacion,FechaSolicitud,EstadoSolicitud,TecnicoId,ClienteId,ActivoId")] Solicitud solicitud)
        {
            if (id != solicitud.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitud);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudExists(solicitud.Id))
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
            ViewData["ActivoId"] = new SelectList(_context.Activo, "Id", "Marca", solicitud.ActivoId);
            ViewData["ClienteId"] = new SelectList(_context.Persona, "Id", "Id", solicitud.ClienteId);
            ViewData["TecnicoId"] = new SelectList(_context.Persona, "Id", "Id", solicitud.TecnicoId);
            return View(solicitud);
        }

        // GET: Tecnica/Solicitudes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitud = await _context.Solicitud
                .Include(s => s.Activo)
                .Include(s => s.Cliente)
                .Include(s => s.Tecnico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // POST: Tecnica/Solicitudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var solicitud = await _context.Solicitud.FindAsync(id);
            _context.Solicitud.Remove(solicitud);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudExists(Guid id)
        {
            return _context.Solicitud.Any(e => e.Id == id);
        }
    }
}
