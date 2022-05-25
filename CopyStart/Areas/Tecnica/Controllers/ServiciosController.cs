using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CopyStart.Data;
using CopyStart.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CopyStart.Areas.Tecnica.Controllers
{
  
    [Area("Tecnica")]
    public class ServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServiciosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tecnica/Servicios
        [Authorize(Roles = "Administrador, Coordinador, Tecnico, Cliente")]
        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(User);
            List<Servicio> listaServicios = null;
            if (User.IsInRole("Cliente"))
            {

                listaServicios = await _context.Servicio.Include(s => s.Activo).ThenInclude(a => a.TipoActivo).Include(s => s.Solicitudes.Cliente).Include(s => s.Soportes).Include(s => s.TipoServicios).Include(s => s.Diagnostico).Where(x => x.Activo.PersonaId == user.PersonaId && x.Estado != "Finalizado").ToListAsync();
            }
            if (User.IsInRole("Tecnico"))
            {

                listaServicios = await _context.Servicio.Include(s => s.Activo).Include(s => s.Solicitudes).ThenInclude(s => s.Cliente).Include(s => s.Soportes).Include(s => s.TipoServicios).Include(s => s.Diagnostico).Where(x => x.Solicitudes.TecnicoId == user.PersonaId && x.Estado != "Finalizado").ToListAsync();
            }



            if (User.IsInRole("Administrador") || User.IsInRole("Coordinador"))
            {
                listaServicios = await _context.Servicio.Include(s => s.Activo).ThenInclude(a => a.TipoActivo).Include(s => s.Solicitudes.Cliente).Include(s => s.Soportes).Include(s => s.TipoServicios).Include(s => s.Diagnostico).ToListAsync();

            }

            return View(listaServicios);

        }

        // GET: Tecnica/Servicios/Details/5
        [Authorize(Roles = "Administrador, Coordinador, Tecnico, Cliente")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio
                .Include(s => s.Activo)                          
                .Include(s => s.Solicitudes)
                .Include(s => s.Soportes)
                .Include(s => s.TipoServicios)
                .Include(s => s.Diagnostico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // GET: Tecnica/Servicios/Create
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
            ViewData["ActivoId"] = new SelectList(_context.Activo, "Id", "MarcaActivo", servicio.ActivoId);
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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
                .Include(s => s.TipoServicios)
                .Include(s => s.Diagnostico)
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
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var servicio = await _context.Servicio.FindAsync(id);
            _context.Servicio.Remove(servicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }





        [Authorize(Roles = "Administrador")]
        private bool ServicioExists(Guid id)
        {
            return _context.Servicio.Any(e => e.Id == id);
        }


        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> ConfirmarServicioAsync(Guid? id)
        {
            var servicio = await _context.Servicio.FindAsync(id);
            servicio.Estado = "En ejecucion";
            _context.Update(servicio);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Servicios", new { area = "Tecnica", id = servicio.Id });


        }


        [Authorize(Roles = "Administrador, Tecnico")]
        public async Task<IActionResult> FinalizarServicioAsync(Guid? id)
        {
            var servicio = await _context.Servicio.FindAsync(id);
            servicio.Estado = "Finalizado";
            servicio.FechaRealizacion = DateTime.Now;
            _context.Update(servicio);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Servicios", new { area = "Tecnica", id = servicio.Id });


        }

       

        [Authorize(Roles = "Administrador, Coordinador, Tecnico, Cliente")]
        public async Task<IActionResult> HistorialPorActivo(Guid? idActivo)
        {

            var servicios = _context.Servicio.Include(s => s.Activo).Include(s => s.Solicitudes.Cliente).Include(s => s.Soportes).Include(s => s.TipoServicios).Include(s => s.Diagnostico).Where(x => x.Activo.Id == idActivo && x.Estado == "Finalizado");
            return View(await servicios.ToListAsync());
        }


        [Authorize(Roles = "Administrador, Coordinador, Tecnico, Cliente")]
        public async Task<IActionResult> Historial()
        {
            var user = await _userManager.GetUserAsync(User);
            List<Servicio> listaServicios = null;
            List<Activo> listaActivos = null;
            if (User.IsInRole("Cliente"))
            {
                listaServicios= await _context.Servicio.Include(s => s.Activo).Include(s => s.Solicitudes.Cliente).Include(s => s.Soportes).Include(s => s.TipoServicios).Include(s => s.Diagnostico).Where(x => x.Activo.PersonaId == user.PersonaId && x.Estado == "Finalizado").ToListAsync();
               

            }
            if (User.IsInRole("Tecnico"))
            {
               listaServicios =await _context.Servicio.Include(s => s.Activo).Include(s => s.Solicitudes.Cliente).Include(s => s.Soportes).Include(s => s.TipoServicios).Include(s => s.Diagnostico).Where(x => x.Solicitudes.TecnicoId == user.PersonaId && x.Estado == "Finalizado").ToListAsync();
               
            }
            else
            {
                listaServicios = await _context.Servicio.Include(s => s.Activo).Include(s => s.Solicitudes.Cliente).Include(s => s.Soportes).Include(s => s.TipoServicios).Include(s => s.Diagnostico).Where(x => x.Estado == "Finalizado").ToListAsync();
                

            }

            foreach (var i in listaServicios)
                {
                    Activo aux;
                    aux = await _context.Activo.FindAsync(i.ActivoId);
                listaActivos.Add(aux);


                }

            return View(listaActivos);
        }


    }
}
