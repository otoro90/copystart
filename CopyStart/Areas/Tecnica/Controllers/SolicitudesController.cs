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
using CopyStart.Models;
using CopyStart.Filters;

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

        [Authorize(Roles = "Administrador, Coordinador, Tecnico, Cliente")]
        [UrlScriptActionFilter]
        public async Task<IActionResult> Index()
        {
            var breadcrumbList = new List<Breadcrumb>();
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Inicio",
                Action = "Index",
                Controller = "Home",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Solicitudes",
                Active = false
            });
            ViewBag.Breadcrumbs = breadcrumbList;

            var user = await _userManager.GetUserAsync(User);
            List<Solicitud> listaSolicitudes=null;
            if (User.IsInRole("Cliente"))
            {

                listaSolicitudes = await _context.Solicitud.Where(e => e.ClienteId == user.PersonaId).Include(s => s.Activo).Include(s => s.Cliente).Include(s => s.Tecnico).ToListAsync();
            }
            if (User.IsInRole("Tecnico"))
            {

               listaSolicitudes =await _context.Solicitud.Where(e => e.TecnicoId == user.PersonaId).Include(s => s.Activo).Include(s => s.Cliente).Include(s => s.Tecnico).ToListAsync();
            }



            if (User.IsInRole("Administrador") || User.IsInRole("Coordinador"))
            {
                listaSolicitudes =await _context.Solicitud.Include(s => s.Activo).ThenInclude(s => s.MarcaActivo).Include(s => s.Cliente).Include(s => s.Tecnico).ToListAsync();
                
            }

            return View(listaSolicitudes);
        }



        





        // GET: Tecnica/Solicitudes/Details/5
        [Authorize(Roles = "Administrador, Coordinador, Tecnico, Cliente")]
        public async Task<IActionResult> Details(long? id)
        {
            var breadcrumbList = new List<Breadcrumb>();
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Inicio",
                Action = "Index",
                Controller = "Home",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Solicitudes",
                Action = "Index",
                Controller = "Solicitudes",
                Area = "Tecnica",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Solicitud "+id.ToString(),
                Active = false
            });
            ViewBag.Breadcrumbs = breadcrumbList;

            if (id == null)
            {
               return NotFound();
            }
            var solicitud = await _context.Solicitud
                .Include(s => s.Activo).ThenInclude(s => s.MarcaActivo)
                .Include(s => s.Activo).ThenInclude(s => s.ModeloActivo)          
                .Include(s => s.Cliente)
                .Include(s => s.Tecnico).
                Include(s=>s.Ubicacion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // GET: Tecnica/Solicitudes/Create
        [Authorize(Roles = "Administrador, Cliente")]
        public IActionResult Create(string idActivo)
        {
            var breadcrumbList = new List<Breadcrumb>();
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Inicio",
                Action = "Index",
                Controller = "Home",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Solicitudes",
                Action = "Index",
                Controller = "Solicitudes",
                Area = "Tecnica",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Crear",
                Action="Create",
                Active = false
            });
            ViewBag.Breadcrumbs = breadcrumbList;

            ViewData["ActivoId"] = new SelectList(_context.Activo.Include(s=>s.MarcaActivo).Include(s=>s.ModeloActivo).Select(x => new { Id = x.Id, Texto = x.MarcaActivo.Nombre + " - " + x.ModeloActivo.Nombre + ". Sn " + x.Serial }), "Id", "Texto", idActivo);
            ViewData["ClienteId"] = new SelectList(_context.Persona, "Id", "Id");
            ViewData["TecnicoId"] = new SelectList(_context.Persona, "Id", "Id");
            ViewData["UbicacionId"] = new SelectList(_context.Ubicacion, "CodigoLugar", "Lugar");

            return View();

        }

        // POST: Tecnica/Solicitudes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Create([Bind("Id,Incidencia,Descripcion,UbicacionId,FechaSolicitud,EstadoSolicitud,TecnicoId,ClienteId,ActivoId")] Solicitud solicitud)
        {
            var user = await _userManager.GetUserAsync(User);
           
            solicitud.ClienteId = (Guid)user.PersonaId;
            solicitud.FechaSolicitud = DateTime.Now;
            solicitud.EstadoSolicitud = "Por tramitar";
            



            if (ModelState.IsValid)
            {

                _context.Add(solicitud);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivoId"] = new SelectList(_context.Activo.Include(s => s.MarcaActivo).Include(s => s.ModeloActivo).Select(x => new { Id = x.Id, Texto = x.MarcaActivo.Nombre + " - " + x.ModeloActivo.Nombre + ". Sn " + x.Serial }), "Id", "Texto");
            ViewData["ClienteId"] = new SelectList(_context.Persona, "Id", "Id", solicitud.ClienteId);
            ViewData["TecnicoId"] = new SelectList(_context.Persona, "Id", "Id", solicitud.TecnicoId);
            ViewData["UbicacionId"] = new SelectList(_context.Ubicacion, "CodigoLugar", "Lugar", solicitud.UbicacionId);

            return View(solicitud);
        }

        // GET: Tecnica/Solicitudes/Edit/5
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Edit(long? id)
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
            ViewData["EstadoSolicitud"] = solicitud.EstadoSolicitud;
            ViewData["FechaSolicitud"] = solicitud.FechaSolicitud;
            ViewData["ActivoId"] = solicitud.ActivoId;
            ViewData["ClienteId"] = solicitud.ClienteId;
            ViewData["TecnicoId"] = solicitud.TecnicoId;
            ViewData["Ubicacion"] = new SelectList(_context.Ubicacion, "CodigoLugar", "Lugar", solicitud.UbicacionId);
            return View(solicitud);
        }

        // POST: Tecnica/Solicitudes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Incidencia,Descripcion,UbicacionId,FechaSolicitud,EstadoSolicitud,TecnicoId,ClienteId,ActivoId")] Solicitud solicitud)
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
            ViewData["ActivoId"] = new SelectList(_context.Activo.Include(s => s.MarcaActivo).Include(s => s.ModeloActivo).Select(x => new { Id = x.Id, Texto = x.MarcaActivo.Nombre + " - " + x.ModeloActivo.Nombre + ". Sn " + x.Serial }), "Id", "Texto", solicitud.ActivoId);
            ViewData["ClienteId"] = new SelectList(_context.Persona, "Id", "Id", solicitud.ClienteId);
            ViewData["TecnicoId"] = new SelectList(_context.Persona, "Id", "Id", solicitud.TecnicoId);
            return View(solicitud);
        }

        // GET: Tecnica/Solicitudes/Delete/5
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Delete(long? id)
        {
            var breadcrumbList = new List<Breadcrumb>();
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Inicio",
                Action = "Index",
                Controller = "Home",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Solicitudes",
                Action = "Index",
                Controller = "Solicitudes",
                Area = "Tecnica",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Eliminar",
                Active = false
            });

            ViewBag.Breadcrumbs = breadcrumbList;
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
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var solicitud = await _context.Solicitud.FindAsync(id);
            _context.Solicitud.Remove(solicitud);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       
        [Authorize(Roles = "Administrador")]
        private bool SolicitudExists(long id)
        {
            return _context.Solicitud.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Administrador, Coordinador")]
        public async Task<IActionResult> TramitarSolicitudAsync(long id)
        {
            var breadcrumbList = new List<Breadcrumb>();
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Inicio",
                Action = "Index",
                Controller = "Home",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Solicitudes",
                Action = "Index",
                Controller = "Solicitudes",
                Area = "Tecnica",
                Active = true
            }); breadcrumbList.Add(new Breadcrumb
            {
                Text = "Solicitud " + id.ToString(),
                Action = "Details",
                Controller = "Solicitudes",
                Area = "Tecnica",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "id", id.ToString() }
                }
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Tramitar",
              
                Active = false
            });
            ViewBag.Breadcrumbs = breadcrumbList;


            var solicitud = await _context.Solicitud
                .Include(s => s.Activo)
                .Include(s => s.Cliente)
                .Include(s => s.Tecnico)
                .FirstOrDefaultAsync(m => m.Id == id);


            return View(solicitud);

        }

        [Authorize(Roles = "Administrador, Coordinador")]
        // GET: Tecnica/Solicitudes/AsignarTecnico
        public async Task<IActionResult> AsignarTecnicoAsync(long? id)
        {
            var breadcrumbList = new List<Breadcrumb>();
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Inicio",
                Action = "Index",
                Controller = "Home",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Solicitudes",
                Action = "Index",
                Controller = "Solicitudes",
                Area = "Tecnica",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Solicitud " + id.ToString(),
                Action = "Details",
                Controller = "Solicitudes",
                Area = "Tecnica",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "id", id.ToString() }
                }
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Tramitar",
                Action = "TramitarSolicitud",
                Controller = "Solicitudes",
                Area = "Tecnica",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "id", id.ToString() }
                }
            });

            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Asignar tecnico",
                Active = false
            });
            ViewBag.Breadcrumbs = breadcrumbList;

            if (id == null)
            {
                return NotFound();
            }          
            var solicitud = await _context.Solicitud.FindAsync(id);

            ViewData["TecnicoId"] = new SelectList(_context.UserRoles.Where(x => x.RoleId == "TEC").Include(a => a.User).ThenInclude(a => a.Persona).Select(x => new { Id = x.User.Persona.Id, Texto = x.User.Persona.Nombres + " " + x.User.Persona.Apellidos + " - " + x.User.Persona.NumeroDocumento }), "Id", "Texto");
            _context.UserRoles.Where(x => x.RoleId == "TEC").Include(a => a.User).ThenInclude(a => a.Persona);

            return View(solicitud);

        }

        
        // POST: Administracion/Personas/AsignarTecnico
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Coordinador")]
        public async Task<IActionResult> AsignarTecnico(long? id,[Bind("TecnicoId")] AsignarTecnico tecnico)
        {
            
            var solicitud = await _context.Solicitud
                .Include(s => s.Activo)
                .Include(s => s.Cliente)
                .Include(s => s.Tecnico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ModelState.IsValid)
            {
                
                solicitud.TecnicoId = tecnico.TecnicoId;
                solicitud.EstadoSolicitud = "Por diagnosticar";
                solicitud.FechaAsignacion=DateTime.Now;
                await _context.SaveChangesAsync();
            }           
            return RedirectToAction("Index", "Solicitudes", new { area = "Tecnica" });

        }





        [Authorize(Roles = "Administrador, Coordinador")]
        public async Task<IActionResult> CancelarSolicitudAsync(long? id)
        {
            var breadcrumbList = new List<Breadcrumb>();
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Inicio",
                Action = "Index",
                Controller = "Home",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Solicitudes",
                Action = "Index",
                Controller = "Solicitudes",
                Area = "Tecnica",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Solicitud " + id.ToString(),
                Action = "Details",
                Controller = "Solicitudes",
                Area = "Tecnica",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "id", id.ToString() }
                }
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Tramitar",
                Action = "TramitarSolicitud",
                Controller = "Solicitudes",
                Area = "Tecnica",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "id", id.ToString() }
                }
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Cancelar solicitud",
                Active = false
            });
            ViewBag.Breadcrumbs = breadcrumbList;



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
        [Authorize(Roles = "Administrador, Coordinador")]
        public async Task<IActionResult> CancelarSolicitud(long? id, [Bind("Descripcion")] EditarDesc desc)
        {
            var solicitud = await _context.Solicitud
                .Include(s => s.Activo)
                .Include(s => s.Cliente)
                .Include(s => s.Tecnico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ModelState.IsValid)
            {

                solicitud.Motivo = desc.Motivo;           
                solicitud.EstadoSolicitud = "Cancelada";
                await _context.SaveChangesAsync();
            }



            return RedirectToAction("Index", "Solicitudes", new { area = "Tecnica" });

        }
        [Authorize(Roles = "Administrador, Coordinador, Tecnico, Cliente")]
        public async Task<IActionResult> VerServicioAsync(long? id)
        {
            

            var servicio = await _context.Servicio
                .Include(s => s.Activo)
                .Include(s => s.Diagnostico)
                .Include(s => s.Solicitudes)
                .FirstOrDefaultAsync(m => m.SolicitudId == id);

            

            return RedirectToAction("Details", "Servicios", new { area = "Tecnica", id = servicio.Id });

        }


    }



}

