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
using CopyStart.Models;
using CopyStart.Filters;
using CopyStart.Areas.Tecnica.Models;
using Microsoft.Extensions.Configuration;

namespace CopyStart.Areas.Tecnica.Controllers
{

    [Area("Tecnica")]
    public class ServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public ServiciosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        // GET: Tecnica/Servicios
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
                Text = "Servicios",
                Action = "Index",
                Controller = "Servicios",
                Area = "Tecnica",
                Active = false
            });
            ViewBag.Breadcrumbs = breadcrumbList;

            var user = await _userManager.GetUserAsync(User);
            List<Servicio> listaServicios = null;
            if (User.IsInRole("Cliente"))
            {

                listaServicios = await _context.Servicio.Include(s => s.Activo).ThenInclude(a => a.TipoActivo).Include(s => s.Solicitudes.Cliente).Include(s => s.TipoServicios).Where(x => x.Activo.PersonaId == user.PersonaId && x.Estado != "Finalizado").ToListAsync();
            }
            if (User.IsInRole("Tecnico"))
            {

                listaServicios = await _context.Servicio.Include(s => s.Activo).ThenInclude(a => a.TipoActivo).Include(s => s.Solicitudes).ThenInclude(s => s.Cliente).Include(s => s.TipoServicios).Where(x => x.Solicitudes.TecnicoId == user.PersonaId && x.Estado != "Finalizado").ToListAsync();
            }
            if (User.IsInRole("Administrador") || User.IsInRole("Coordinador"))
            {
                listaServicios = await _context.Servicio.Include(s => s.Activo).ThenInclude(a => a.TipoActivo).Include(s => s.Solicitudes.Cliente).Include(s => s.TipoServicios).ToListAsync();

            }

            return View(listaServicios);

        }

        // GET: Tecnica/Servicios/Details/5
        [Authorize(Roles = "Administrador, Coordinador, Tecnico, Cliente")]
        public async Task<IActionResult> Details(long? id)
        {
                

 
            var servicio = await _context.Servicio
                            .Include(s => s.Activo).ThenInclude(x => x.Persona)
                            .Include(s => s.Activo.MarcaActivo)
                            .Include(s => s.Activo.ModeloActivo)
                            .Include(s => s.Solicitudes.Tecnico)
                            .Include(s => s.Solicitudes)
                            .Include(s => s.TipoServicios).Include(s=>s.Archivos).ThenInclude(u => u.Archivo)
                            .FirstOrDefaultAsync(m => m.Id == id);
          

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
                Text = "Servicios",
                Action = "Index",
                Controller = "Servicios",
                Area = "Tecnica",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Servicio " + servicio.Id,
                Active = false,

            });

            ViewBag.Breadcrumbs = breadcrumbList;


            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // GET: Tecnica/Servicios/Create
        [Authorize(Roles = "Administrador, Tecnico, Coordinador")]
        public IActionResult Create(long? idSolicitud)
        {
            var solicitud = _context.Solicitud.Include(x => x.Activo).FirstOrDefault(x => x.Id == idSolicitud);
            if (solicitud.Activo.Serial == null)
            {
                return RedirectToAction("AgregarSerial", "Activos", new { area = "Activos", id = solicitud.ActivoId });
            }
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
                Text = "Servicios",
                Action = "Index",
                Controller = "Servicios",
                Area = "Tecnica",
                Active = true
            }); breadcrumbList.Add(new Breadcrumb
            {
                Text = "Crear",
                Active = false,

            });

            ViewBag.Breadcrumbs = breadcrumbList;

            ViewData["ActivoId"] = new SelectList(_context.Activo.Where(x => x.Estado != "Eliminado"), "Id", "Serial", solicitud.ActivoId);
            ViewData["SolicitudId"] = new SelectList(_context.Solicitud, "Id", "Incidencia", idSolicitud);
            ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio.Where(x => x.TipoActivoId == solicitud.Activo.TipoActivoId), "Id", "Nombre");




            return View();
        }

        // POST: Tecnica/Servicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Tecnico, Coordinador")]
        public async Task<IActionResult> Create([Bind("Id,ActivoId,SolicitudId,TipoServicioId")] Servicio servicio)
        {
            servicio.Estado = "Por confirmar";


            if (ModelState.IsValid)
            {


                _context.Add(servicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivoId"] = new SelectList(_context.Activo, "Id", "Serial", servicio.ActivoId);
            ViewData["SolicitudId"] = new SelectList(_context.Solicitud, "Id", "Incidencia", servicio.SolicitudId);
            return View(servicio);
        }

        // GET: Tecnica/Servicios/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(long? id)
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
            ViewData["ActivoId"] = new SelectList(_context.Activo, "Id", "Serial", servicio.ActivoId);
            ViewData["SolicitudId"] = new SelectList(_context.Solicitud, "Id", "Incidencia", servicio.SolicitudId);
            return View(servicio);
        }

        // POST: Tecnica/Servicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Estado,FechaRealizacion,DiagnosticoId,ActivoId,SolicitudId,SoportesId")] Servicio servicio)
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
            ViewData["ActivoId"] = new SelectList(_context.Activo, "Id", "Serial", servicio.ActivoId);
            ViewData["SolicitudId"] = new SelectList(_context.Solicitud, "Id", "Incidencia", servicio.SolicitudId);
            return View(servicio);
        }

        // GET: Tecnica/Servicios/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio
                .Include(s => s.Activo)
                .Include(s => s.Solicitudes)
                .Include(s => s.TipoServicios)
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
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var servicio = await _context.Servicio.FindAsync(id);
            _context.Servicio.Remove(servicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }





        [Authorize(Roles = "Administrador")]
        private bool ServicioExists(long id)
        {
            return _context.Servicio.Any(e => e.Id == id);
        }


        [Authorize(Roles = "Administrador, Tecnico, Coordinador")]
        public async Task<IActionResult> ConfirmarServicio(long? id)
        {
            var servicio = await _context.Servicio
               .Include(s => s.Activo)
               .Include(s => s.Solicitudes)
               .FirstOrDefaultAsync(m => m.Id == id);
            var tecnico = await _context.Persona.FindAsync(servicio.Solicitudes.TecnicoId);
            tecnico.Estado = "En servicio";
            servicio.Estado = "En ejecucion";
            servicio.Solicitudes.EstadoSolicitud = "En servicio";
            servicio.FechaInicio = DateTime.Now;
            _context.Update(servicio);
            _context.Update(tecnico);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Servicios", new { area = "Tecnica", id = servicio.Id });


        }


        [Authorize(Roles = "Administrador, Tecnico, Coordinador")]
        public async Task<IActionResult> FinalizarServicioAsync(long? id)
        {
            var servicio = await _context.Servicio
                .Include(s => s.Activo)
                .Include(s => s.Solicitudes)
                .FirstOrDefaultAsync(m => m.Id == id);
            var tecnico = await _context.Persona.FindAsync(servicio.Solicitudes.TecnicoId);
            tecnico.Estado = "Disponible";

            servicio.Estado = "Finalizado";
            servicio.FechaFinalizacion = DateTime.Now;
            servicio.Solicitudes.EstadoSolicitud = "Servicios Finalizados";


            _context.Update(servicio);
            _context.Update(tecnico);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Servicios", new { area = "Tecnica", id = servicio.Id });


        }



        [Authorize(Roles = "Administrador, Coordinador, Tecnico, Cliente")]
        [UrlScriptActionFilter]
        public async Task<IActionResult> HistorialPorActivo(long? idActivo)
        {

            var servicios = _context.Servicio.Include(s => s.Activo).ThenInclude(a => a.TipoActivo).Include(s => s.Solicitudes.Cliente).Include(s => s.TipoServicios).Where(x => x.Activo.Id == idActivo && x.Estado == "Finalizado");
            var activo = await _context.Activo.Include(a => a.TipoActivo).Include(a => a.MarcaActivo).Include(a => a.ModeloActivo).FirstOrDefaultAsync(a => a.Id == idActivo);

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
                Text = "Activos",
                Action = "Index",
                Controller = "Activos",
                Area = "Activos",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Activo " + idActivo.ToString(),
                Action = "Details",
                Controller = "Activos",
                Area = "Activos",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "id", idActivo.ToString() }
                }
            }); breadcrumbList.Add(new Breadcrumb
            {
                Text = "Historial ",
                Active = false,

            });

            ViewBag.Breadcrumbs = breadcrumbList;
            ViewData["ActivoDatos"] = activo.MarcaActivo.Nombre + " " + activo.ModeloActivo.Nombre + " S/N:" + activo.Serial;
            return View(await servicios.ToListAsync());
        }


        [Authorize(Roles = "Administrador, Coordinador, Tecnico, Cliente")]
        [UrlScriptActionFilter]
        public async Task<IActionResult> Historial()
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
                Text = "Servicios",
                Action = "Index",
                Controller = "Servicios",
                Area = "Tecnica",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Historial",
                Active = false,

            });

            ViewBag.Breadcrumbs = breadcrumbList;


            var user = await _userManager.GetUserAsync(User);
            List<Servicio> listaServicios = null;
            if (User.IsInRole("Cliente"))
            {
                listaServicios = await _context.Servicio.Include(s => s.Activo).ThenInclude(a => a.TipoActivo).Include(s => s.Solicitudes.Cliente).Include(s => s.TipoServicios).Where(x => x.Activo.PersonaId == user.PersonaId && x.Estado == "Finalizado").ToListAsync();


            }
            if (User.IsInRole("Tecnico"))
            {
                listaServicios = await _context.Servicio.Include(s => s.Activo).ThenInclude(a => a.TipoActivo).Include(s => s.Solicitudes.Cliente).Include(s => s.TipoServicios).Where(x => x.Solicitudes.TecnicoId == user.PersonaId && x.Estado == "Finalizado").ToListAsync();

            }
            else
            {
                listaServicios = await _context.Servicio.Include(s => s.Activo).ThenInclude(a => a.TipoActivo).Include(s => s.Solicitudes.Cliente).Include(s => s.TipoServicios).Where(x => x.Estado == "Finalizado").ToListAsync();


            }



            return View(listaServicios);
        }


        [Authorize(Roles = "Tecnico, Administrador, Coordinador")]
        [UrlScriptActionFilter]
        public IActionResult EjecucionProcedimientos(long idServicio)
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
                Text = "Servicios",
                Action = "Index",
                Controller = "Servicios",
                Area = "Tecnica",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Servicio " + idServicio,
                Active = true,
                Action = "Details",
                Controller = "Servicios",
                Area = "Tecnica",
                Params = new Dictionary<string, string>
                {
                    { "id", idServicio.ToString() }
                }

            }); breadcrumbList.Add(new Breadcrumb
            {
                Text = "Procedimientos",
                Active = false
            });

            ViewBag.Breadcrumbs = breadcrumbList;


            try
            {
                var procedimientoTipoServicio = _context.ProcedimientoTipoServicio.Include(x => x.Procedimientos).Join(
                                                                                _context.Servicio.Where(x => x.Id == idServicio),
                                                                                p => p.TipoServicioId,
                                                                                s => s.TipoServicioId,
                                                                                (p, s) => p
                                                                                ).ToList();

                var servicioProcedimientoTipoServicio = _context.ServicioProcedimientoTipoServicio.Include(x => x.Servicio)
                                                                                                  .Include(x => x.ProcedimientoTipoServicio)
                                                                                                  .Include(x => x.ProcedimientoTipoServicio.TipoServicio)
                                                                                                  .Include(x => x.ProcedimientoTipoServicio.Procedimientos)
                                                                                .Where(e => e.ServicioId == idServicio).ToList();

                var result = from procedimiento in procedimientoTipoServicio
                             join servicioProcedimiento in servicioProcedimientoTipoServicio on procedimiento.Id equals servicioProcedimiento.ProcedimientoTipoServicioId into ServiciosProcedimientos
                             from m in ServiciosProcedimientos.DefaultIfEmpty()

                select new ServicioProcedimientoTipoServicio
                    {
                        Id = m?.Id ?? new Guid("00000000-0000-0000-0000-000000000000"),
                        ProcedimientoTipoServicioId = procedimiento.Id,
                        ProcedimientoTipoServicio = procedimiento,
                        ServicioId = idServicio,
                        ProcedimientosRealizados = m?.ProcedimientosRealizados ?? false,
                        Observaciones = m?.Observaciones
                    };

                return View(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [Authorize(Roles = "Tecnico, Administrador, Coordinador")]
        [HttpPost]
        public async Task<IActionResult> EjecucionProcedimientosAsync([FromBody] List<EjecucionProcedimientosVM> data)
        {
            if (!ModelState.IsValid)
                return BadRequest("Enter required fields");

            try
            {
                data.ForEach(x =>
                {
                    if(x.Id == "00000000-0000-0000-0000-000000000000")
                    {
                        var servicioProcedimientoTipoServicio = new ServicioProcedimientoTipoServicio()
                        {
                            Id = Guid.NewGuid(),
                            ServicioId = long.Parse(x.ServicioId),
                            ProcedimientoTipoServicioId = new Guid(x.ProcedimientoTipoServicioId),
                            ProcedimientosRealizados = x.ProcedimientosRealizados,
                            Observaciones = x.Observaciones
                        }; 
                        
                        _context.ServicioProcedimientoTipoServicio.Add(servicioProcedimientoTipoServicio);
                    }
                    else
                    {
                        var servicioProcedimientoTipoServicio =  _context.ServicioProcedimientoTipoServicio.FirstOrDefault(p=>p.Id == new Guid(x.Id));
                        servicioProcedimientoTipoServicio.ProcedimientosRealizados = x.ProcedimientosRealizados;
                        servicioProcedimientoTipoServicio.Observaciones = x.Observaciones;
                    }
                });

                await _context.SaveChangesAsync();

                return Json("Transaccion realizada satisfactoriamente");
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        [Authorize(Roles = "Administrador, Coordinador, Tecnico, Cliente")]
        [UrlScriptActionFilter]
        public async Task<IActionResult> ServiciosPorSolicitud(long? idSolicitud)
        {

            var servicios = _context.Servicio.Include(s => s.Activo).ThenInclude(a => a.TipoActivo).Include(s => s.Solicitudes.Cliente).Include(s => s.TipoServicios).Where(x => x.SolicitudId == idSolicitud);


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
                Text = "Activos",
                Action = "Index",
                Controller = "Activos",
                Area = "Activos",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Solicitud " + idSolicitud.ToString(),
                Action = "Details",
                Controller = "Activos",
                Area = "Activos",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "id", idSolicitud.ToString() }
                }
            }); breadcrumbList.Add(new Breadcrumb
            {
                Text = "Servicios por solicitud",
                Active = false,

            });

            ViewBag.Breadcrumbs = breadcrumbList;

            return View(await servicios.ToListAsync());
        }

        [Authorize(Roles = "Administrador, Coordinador, Tecnico")]
        public async Task<IActionResult> AgregarObservaciones(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           
            return View();
        }

        // POST: Tecnica/Servicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Coordinador, Tecnico")]
        public async Task<IActionResult> AgregarObservaciones(long id, [Bind("Observaciones,File")] AggObservaciones Ob)
        {


            var servicio = await _context.Servicio.FindAsync(id);
            if (id != servicio.Id)
            {
                return NotFound();
            }
            if (((User.IsInRole("Tecnico")) && (servicio.Estado != "En ejecucion")) || ((User.IsInRole("Coordinador")) && (servicio.Estado != "Por confirmar")))
            {
                servicio.Observaciones = Ob.Observaciones;


                if (ModelState.IsValid)
                {
                    _context.Update(servicio);
                    await _context.SaveChangesAsync();

                }


                foreach (var a in Ob.File)
                {
                    var archivo = new Archivo();
                    archivo.Tipo = a.FileName.Split(".").Last();
                    archivo.Nombre = a.FileName.Substring(0, a.FileName.Length - (archivo.Tipo.Length + 1));
                    archivo.Peso = a.Length;
                    if (ModelState.IsValid)
                    {
                        archivo.Id = Guid.NewGuid();
                        _context.Add(archivo);
                        await _context.SaveChangesAsync();
                        var basePath = _configuration["PathBaseFiles"] + "/" + archivo.Id;

                        using (var fileStream = System.IO.File.Create(basePath))
                        {
                            await a.CopyToAsync(fileStream);
                        }
                        var arcser = new ArchivoServicio { ArchivoId = archivo.Id, ServicioId = servicio.Id };
                        _context.Add(arcser);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ViewData["Observaciones"] = servicio.Observaciones;
                        return View(servicio);
                    }

                }
            }
            return RedirectToAction("Details", "Servicios", new { area = "Tecnica", id = id });

        }

        [Authorize(Roles = "Administrador, Coordinador, Tecnico")]
        public async Task<IActionResult> DeleteSoporte(long idservicio, Guid idsoporte)
        {
            var servicio = await _context.Servicio.FindAsync(idservicio);
            if ((servicio.Estado == "Finalizado")&&(User.IsInRole("Tecnico")))
            {
                return BadRequest();
            }


            var archivoServicio = await _context.ArchivoServicio.FindAsync(idservicio, idsoporte);
            _context.ArchivoServicio.Remove(archivoServicio);
            var archivo = await _context.Archivo.FindAsync(idsoporte);
            var basePath = _configuration["PathBaseFiles"] + "/" + archivo.Id;
            System.IO.File.Delete(basePath);
            _context.Archivo.Remove(archivo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Servicios", new { area = "Tecnica", id = idservicio });
        }


    }
}

