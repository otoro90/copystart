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
            List<Solicitud> listaSolicitudes = null;
            if (User.IsInRole("Cliente") || User.IsInRole("Tecnico"))
            {

                listaSolicitudes = await _context.Solicitud.Where(e => (e.ClienteId == user.PersonaId) || (e.TecnicoId == user.PersonaId)).Include(s => s.Activo).Include(s => s.Cliente).Include(s => s.Tecnico).ToListAsync();
            }
            if (User.IsInRole("Administrador") || User.IsInRole("Coordinador"))
            {
                listaSolicitudes = await _context.Solicitud.Include(s => s.Activo).ThenInclude(s => s.MarcaActivo).Include(s => s.Cliente).Include(s => s.Tecnico).ToListAsync();

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
                Text = "Solicitud " + id.ToString(),
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
                 .Include(s => s.Activo).ThenInclude(s => s.Ubicacion)
                .Include(s => s.Cliente)
                .Include(s => s.Tecnico).
                FirstOrDefaultAsync(m => m.Id == id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return View(solicitud);
        }

        // GET: Tecnica/Solicitudes/Create
        [Authorize(Roles = "Administrador, Cliente, Coordinador")]
        [UrlScriptActionFilter]
        public async Task<IActionResult> Create(string idActivo, Guid? idCliente)
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
                Action = "Create",
                Active = false
            });
            ViewBag.Breadcrumbs = breadcrumbList;

            if (idCliente==null)
            {
                var user =  await _userManager.GetUserAsync(User);
                idCliente = user.PersonaId;
            }

            ViewData["ClienteId"] = new SelectList(_context.Persona, "Id", "Id", idCliente);
            
            ViewData["TecnicoId"] = new SelectList(_context.Persona, "Id", "Id");

            ViewData["ActivoId"] = new SelectList(_context.Set<Activo>().Include(s => s.MarcaActivo).Include(s => s.ModeloActivo).Where(a => (a.Estado != "Eliminado") && (a.PersonaId == idCliente)).Select(x => new { Id = x.Id, Texto = x.MarcaActivo.Nombre + " - " + x.ModeloActivo.Nombre + ". Sn " + x.Serial }), "Id", "Texto", idActivo);
            
           

            return View();

        }

        // POST: Tecnica/Solicitudes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UrlScriptActionFilter]
        [Authorize(Roles = "Administrador, Cliente, Coordinador")]
        public async Task<IActionResult> Create([Bind("Id,Incidencia,Descripcion,FechaSolicitud,EstadoSolicitud,TecnicoId,ClienteId,ActivoId")] Solicitud solicitud)
        {
            solicitud.FechaSolicitud = DateTime.Now;
            solicitud.EstadoSolicitud = "Por tramitar";
            if (User.IsInRole("Cliente"))
            {
                var user = await _userManager.GetUserAsync(User);
                solicitud.ClienteId = (Guid)user.PersonaId;
            }

            if (ModelState.IsValid)
            {

                _context.Add(solicitud);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivoId"] = new SelectList(_context.Set<Activo>().Include(s => s.MarcaActivo).Include(s => s.ModeloActivo).Where(a => (a.Estado != "Eliminado") && (a.PersonaId == solicitud.ClienteId)).Select(x => new { Id = x.Id, Texto = x.MarcaActivo.Nombre + " - " + x.ModeloActivo.Nombre + ". Sn " + x.Serial }), "Id", "Texto", solicitud.ActivoId);
            ViewData["ClienteId"] = new SelectList(_context.Persona, "Id", "Id", solicitud.ClienteId);
            ViewData["TecnicoId"] = new SelectList(_context.Persona, "Id", "Id", solicitud.TecnicoId);


            return View(solicitud);
        }

        // GET: Tecnica/Solicitudes/Edit/5
        [Authorize(Roles = "Administrador, Cliente, Coordinador")]
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
            return View(solicitud);
        }

        // POST: Tecnica/Solicitudes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Cliente, Coordinador")]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Incidencia,Descripcion,FechaSolicitud,EstadoSolicitud,TecnicoId,ClienteId,ActivoId")] Solicitud solicitud)
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

            ViewData["TecnicoId"] = new SelectList(_context.UserRoles.Where(x => x.RoleId == "TEC" && x.User.Persona.Estado != "En servicio").Include(a => a.User).ThenInclude(a => a.Persona).Select(x => new { Id = x.User.Persona.Id, Texto = x.User.Persona.Nombres + " " + x.User.Persona.Apellidos + " - " + x.User.Persona.NumeroDocumento }), "Id", "Texto");
            _context.UserRoles.Where(x => x.RoleId == "TEC").Include(a => a.User).ThenInclude(a => a.Persona);

            return View(solicitud);

        }


        // POST: Administracion/Personas/AsignarTecnico
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Coordinador")]
        public async Task<IActionResult> AsignarTecnico(long? id, [Bind("TecnicoId")] AsignarTecnico tecnico)
        {

            var solicitud = await _context.Solicitud
                .Include(s => s.Activo)
                .Include(s => s.Cliente)
                .Include(s => s.Tecnico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ModelState.IsValid)
            {

                solicitud.TecnicoId = tecnico.TecnicoId;
                solicitud.EstadoSolicitud = "Asignada";
                solicitud.FechaAsignacion = DateTime.Now;
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
                .Include(s => s.Activo).ThenInclude(s => s.ModeloActivo).ThenInclude(s => s.MarcaActivo)
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
        public async Task<IActionResult> CancelarSolicitud(long? id, [Bind("Motivo")] EditarDesc desc)
        {
            var solicitud = await _context.Solicitud
                .Include(s => s.Activo)
                .Include(s => s.Cliente)
                .Include(s => s.Tecnico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ModelState.IsValid)
            {

                solicitud.MotivoCancelacion = desc.Motivo;
                solicitud.EstadoSolicitud = "Cancelada";
                await _context.SaveChangesAsync();
            }



            return RedirectToAction("Index", "Solicitudes", new { area = "Tecnica" });

        }
        [Authorize(Roles = "Administrador, Coordinador, Tecnico, Cliente")]
        public async Task<IActionResult> VerServicios(long? id)
        {


            var servicios = await _context.Servicio
                .Include(s => s.Activo)
                .Include(s => s.Solicitudes)
                .Where(m => m.SolicitudId == id).ToListAsync();

            if (servicios.LongCount() != 0)
            {
                if (servicios.LongCount() == 1)
                {
                    var servicio = await _context.Servicio.FirstOrDefaultAsync(s => s.SolicitudId == id);
                    return RedirectToAction("Details", "Servicios", new { area = "Tecnica", id = servicio.Id });
                }
                else
                {
                    return RedirectToAction("ServiciosPorSolicitud", "Servicios", new { area = "Tecnica", idSolicitud = id });
                }

            }


            return RedirectToAction("Details", "Solicitudes", new { area = "Tecnica", id = id });


        }


        [Authorize(Roles = "Administrador, Cliente, Coordinador")]
        [UrlScriptActionFilter]
        public async Task<IActionResult> CrearSolicitudRapida(Guid? personaId, long? activoId)
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
                Text = "Solicitud rapida",
                Action = "Create",
                Active = false
            });
            ViewBag.Breadcrumbs = breadcrumbList;


            var persona = await _context.Persona.FirstOrDefaultAsync(m => m.Id == personaId);

            var solicitud = new SolicitudRapidaModel
            {
                Persona = persona

            };


            ViewData["TipoDocumentoId"] = new SelectList(_context.TipoDocumento, "Id", "Nombre");
            ViewData["TipoActivoId"] = new SelectList(_context.TipoActivo, "Id", "Nombre");
            ViewData["MarcaActivoId"] = new SelectList(_context.MarcaActivo, "Id", "Nombre");
            ViewData["ModeloActivoId"] = new SelectList(_context.ModeloActivo, "Id", "Nombre");
            ViewData["DepartamentosList"] = new SelectList(_context.Ubicacion.Select(x => new { x.CodigoDepartamento, x.Departamento }).Distinct().ToList(), "CodigoDepartamento", "Departamento");


            return View(solicitud);

        }

        // POST: Tecnica/Solicitudes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UrlScriptActionFilter]
        [Authorize(Roles = "Administrador, Cliente, Coordinador")]
        public async Task<IActionResult> CrearSolicitudRapida([Bind("Persona,Activo,Solicitud")] SolicitudRapidaModel solicitudform)
        {
            solicitudform.Activo.Ubicacion = null;
            solicitudform.Persona.Ubicacion = null;
            if (!ModelState.IsValid)
            {
                return View(solicitudform);
            }

            //aqui se pide el documento
            var personaTask = _context.Persona.Where(p => (p.NumeroDocumento == solicitudform.Persona.NumeroDocumento) && (p.TipoDocumentoId == solicitudform.Persona.TipoDocumentoId)).FirstOrDefaultAsync();

            var activoTask = _context.Activo.Where(p => p.Serial == solicitudform.Activo.Serial).FirstOrDefaultAsync();

            var persona = await personaTask;
            var activo = await activoTask;

            if (persona == null)
            {
                persona = solicitudform.Persona;
            }

            if (activo == null)
            {
                activo = solicitudform.Activo;
                activo.Persona = persona;
                activo.FechaRegistro = DateTime.Now;
                activo.Estado = "Inactivo";
            }

            var solicitud = new Solicitud()
            {
                Incidencia = solicitudform.Solicitud.Incidencia,
                Descripcion = solicitudform.Solicitud.Descripcion,
                Cliente = persona,
                Activo = activo,
                FechaSolicitud = DateTime.Now,
                Direccion = activo.Direccion,              
                EstadoSolicitud = "Por tramitar",
            };
            await _context.Solicitud.AddAsync(solicitud);

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = solicitud.Id });

        }
    }
}


