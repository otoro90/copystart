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
using CopyStart.Areas.Tecnica.Models;

namespace CopyStart.Areas.Tecnica.Controllers
{
    [Area("Tecnica")]

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

        [Authorize(Policy = "VerListadoCompletoSolicitudes")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Solicitud.Include(s => s.Activo).Include(s => s.Cliente).Include(s => s.Tecnico);
            var solicitudes = await applicationDbContext.ToListAsync();

            return View(solicitudes);
        }

        // GET: Tecnica/Solicitudes/Details/5
        [Authorize(Policy = "VerInformacionSolicitudes")]
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
        [Authorize(Policy = "CrearSolicitudes")]
        public IActionResult Create(string idActivo)
        {
            ViewData["ActivoId"] = new SelectList(_context.Activo.Select(x => new { Id = x.Id, Texto = x.Marca + " - " + x.Modelo + ". Sn " + x.Serial }), "Id", "Texto", idActivo);
            ViewData["ClienteId"] = new SelectList(_context.Persona, "Id", "Id");
            ViewData["TecnicoId"] = new SelectList(_context.Persona, "Id", "Id");
            
            return View();

        }

        // POST: Tecnica/Solicitudes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CrearSolicitudes")]
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
        [Authorize(Policy = "EditarSolicitudes")]
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
        [Authorize(Policy = "EditarSolicitudes")]
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
        [Authorize(Policy = "EliminarSolicitudes")]
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
        [Authorize("EliminarSolicitudes")]
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

        [Authorize(Policy = "TramitarSolicitudes")]
        public async Task<IActionResult> TramitarSolicitudAsync(Guid id)
        {
         

            var solicitud = await _context.Solicitud
                .Include(s => s.Activo)
                .Include(s => s.Cliente)
                .Include(s => s.Tecnico)
                .FirstOrDefaultAsync(m => m.Id == id);


            return View(solicitud);

        }

        [Authorize(Policy = "AsignarTecnicoASolicitudes")]
        // GET: Tecnica/Solicitudes/AsignarTecnico
        public async Task<IActionResult> AsignarTecnicoAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

          
            var solicitud = await _context.Solicitud.FindAsync(id);
          
            
            ViewData["TecnicoId"] = new SelectList(_context.Persona.Select(x=> new {Id = x.Id , Texto = x.Nombres+" "+x.Apellidos+" - "+ x.NumeroDocumento}), "Id", "Texto" );
          
            return View(solicitud);

        }

        
        // POST: Administracion/Personas/AsignarTecnico
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AsignarTecnicoASolicitudes")]
        public async Task<IActionResult> AsignarTecnico(Guid? id,[Bind("TecnicoId")] AsignarTecnico tecnico)
        {
            var solicitud = await _context.Solicitud
                .Include(s => s.Activo)
                .Include(s => s.Cliente)
                .Include(s => s.Tecnico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ModelState.IsValid)
            {
                solicitud.EstadoSolicitud = "Tramitada";
                solicitud.TecnicoId = tecnico.TecnicoId;
                await _context.SaveChangesAsync();
            }


           
            return RedirectToAction("Index", "Solicitudes", new { area = "Tecnica" });

        }





        [Authorize(Policy = "CancelarSolicitudes")]
        public async Task<IActionResult> CancelarSolicitudAsync(Guid? id)
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

            ViewData["Descripcion"] = solicitud.Descripcion;
            
            return View(solicitud);

        }


        // POST: Administracion/Personas/AsignarTecnico
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CancelarSolicitudes")]
        public async Task<IActionResult> CancelarSolicitud(Guid? id, [Bind("Descripcion")] EditarDesc desc)
        {
            var solicitud = await _context.Solicitud
                .Include(s => s.Activo)
                .Include(s => s.Cliente)
                .Include(s => s.Tecnico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ModelState.IsValid)
            {
                solicitud.EstadoSolicitud = "Cancelada";
                solicitud.Descripcion = desc.Descripcion;
                await _context.SaveChangesAsync();
            }



            return RedirectToAction("Index", "Solicitudes", new { area = "Tecnica" });

        }
        [Authorize(Policy = "VerServicioDeUnaSolicitud")]
        public async Task<IActionResult> VerServicioAsync(Guid? id)
        {
            

            var servicio = await _context.Servicio
                .Include(s => s.Activo)
                .Include(s => s.Diagnostico)
                .Include(s => s.Solicitudes)
                .FirstOrDefaultAsync(m => m.SolicitudId == id);

            

            return RedirectToAction("Details", "Servicios", new { area = "Tecnica", id = servicio.Id });

        }



        [Authorize(Policy = "VerListadoPropioDeSolicitudesRealizadas")]
        public async Task<IActionResult> SolicitudesRealizadas()
        {
           
            var user = await _userManager.GetUserAsync(User);

            var persona = await _context.Persona.Include(x => x.TipoDocumento).Where(x => x.Id == user.PersonaId).FirstOrDefaultAsync();
            var applicationDbContext = _context.Solicitud.Where(e => e.ClienteId == persona.Id).Include(s => s.Activo).Include(s => s.Cliente).Include(s => s.Tecnico);
            var solicitudes = await applicationDbContext.ToListAsync();

            return View(solicitudes);
        }

        [Authorize(Policy = "VerListadoPropioDeSolicitudesAsignadas")]
        public async Task<IActionResult> SolicitudesAsignadas()
        {

            var user = await _userManager.GetUserAsync(User);

            var persona = await _context.Persona.Include(x => x.TipoDocumento).Where(x => x.Id == user.PersonaId).FirstOrDefaultAsync();
            var applicationDbContext = _context.Solicitud.Where(e => e.TecnicoId == persona.Id).Include(s => s.Activo).Include(s => s.Cliente).Include(s => s.Tecnico);
            var solicitudes = await applicationDbContext.ToListAsync();

            return View(solicitudes);
        }







    }



}

