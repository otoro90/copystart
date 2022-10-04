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
using CopyStart.Areas.Administracion.Models;
using CopyStart.Models;
using CopyStart.Filters;

namespace CopyStart.Areas.Administracion.Controllers
{
    [Area("Administracion")]
    [Authorize(Roles = "Administrador")]
    public class ProcedimientoTipoServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProcedimientoTipoServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administracion/ProcedimientoTipoServicios
        [UrlScriptActionFilter]
        public async Task<IActionResult> Index(Guid? tipoServicioId)

        {


            List<ProcedimientoTipoServicio> listaProcedimientoTipoServicio;
            if (tipoServicioId != null)
            {
                listaProcedimientoTipoServicio = await _context.ProcedimientoTipoServicio.Include(p => p.Procedimientos).Include(p => p.TipoServicio).Where(e => e.TipoServicioId == tipoServicioId).ToListAsync();
            }
            else
            {
                listaProcedimientoTipoServicio = await _context.ProcedimientoTipoServicio.Include(p => p.Procedimientos).Include(p => p.TipoServicio).ToListAsync();
            }

            var tipoServicio = await _context.TipoServicio.FirstOrDefaultAsync(m => m.Id == tipoServicioId);

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
                Text = "Tipos de servicio",
                Action = "Index",
                Controller = "TipoServicios",
                Area = "Parametricas",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = tipoServicio.Nombre,
                Action = "Details",
                Controller = "TipoServicios",
                Area = "Parametricas",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "id", tipoServicioId.ToString() }
                }
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Procedimientos",
                Action = "Index",
                Controller = "ProcedimientoTipoServicios",
                Area = "Administracion",
                Active = false,
                Params = new Dictionary<string, string>
                {
                    { "tipoServicioId", tipoServicioId.ToString() }
                }
            });


            ViewBag.Breadcrumbs = breadcrumbList;
            ViewData["TipoServicioId"] = tipoServicioId;
            return View(listaProcedimientoTipoServicio);
        }

        // GET: Administracion/ProcedimientoTipoServicios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimientoTipoServicio = await _context.ProcedimientoTipoServicio
                .Include(p => p.Procedimientos)
                .Include(p => p.TipoServicio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (procedimientoTipoServicio == null)
            {
                return NotFound();
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
                Text = "Tipos de servicio",
                Action = "Index",
                Controller = "TipoServicios",
                Area = "Parametricas",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = procedimientoTipoServicio.TipoServicio.Nombre,
                Action = "Details",
                Controller = "TipoServicios",
                Area = "Parametricas",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "id", procedimientoTipoServicio.TipoServicioId.ToString() }
                }
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Procedimientos",
                Action = "Index",
                Controller = "ProcedimientoTipoServicios",
                Area = "Administracion",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "tipoServicioId", procedimientoTipoServicio.TipoServicioId.ToString() }
                }
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Detalles",
              
                Active = false
                
            });


            ViewBag.Breadcrumbs = breadcrumbList;

            return View(procedimientoTipoServicio);
        }

        // GET: Administracion/ProcedimientoTipoServicios/Create
        public async Task<IActionResult> Create(Guid? tipoServicioId)
        {
            var tipoServicio = await _context.TipoServicio.FindAsync(tipoServicioId);


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
                Text = "Tipos de servicio",
                Action = "Index",
                Controller = "TipoServicios",
                Area = "Parametricas",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = tipoServicio.Nombre,
                Action = "Details",
                Controller = "TipoServicios",
                Area = "Parametricas",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "id", tipoServicioId.ToString() }
                }
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Procedimientos",
                Action = "Index",
                Controller = "ProcedimientoTipoServicios",
                Area = "Administracion",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "tipoServicioId", tipoServicioId.ToString() }
                }
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Crear",

                Active = false

            });


            ViewBag.Breadcrumbs = breadcrumbList;


            if (tipoServicioId != null)
            {
                ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Nombre", tipoServicio.Id);
            }
            else
            {
                ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Nombre");
            }
            ViewData["ProcedimientoId"] = new SelectList(_context.Procedimiento, "Id", "Codigo");           
            return View();
        }

        // POST: Administracion/ProcedimientoTipoServicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Numero,TipoServicioId,Id,Nombre,Descripcion,Codigo,Estado,TiempoEjecucion")] ProcedimientoCrear procedimientoCrear)
        {
            if (ModelState.IsValid)
            {

                var procedimiento = new Procedimiento
                {
                    Id = Guid.NewGuid(),
                    Nombre = procedimientoCrear.Nombre,
                    Codigo = procedimientoCrear.Codigo,
                    Descripcion = procedimientoCrear.Descripcion,
                    Estado = procedimientoCrear.Estado,
                    TiempoEjecucion = procedimientoCrear.TiempoEjecucion
                };

                var procedimientoTipoServicio = new ProcedimientoTipoServicio
                {
                    Id = Guid.NewGuid(),
                    TipoServicioId = procedimientoCrear.TipoServicioId,
                    ProcedimientoId = procedimiento.Id,
                    Numero=procedimientoCrear.Numero

                };
                _context.Add(procedimiento);
                _context.Add(procedimientoTipoServicio);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "ProcedimientoTipoServicios", new { area = "Administracion", tipoServicioId = procedimientoTipoServicio.TipoServicioId });

            }

            
            ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Codigo", procedimientoCrear.TipoServicioId);
            return View(procedimientoCrear);




        }
            
        

        // GET: Administracion/ProcedimientoTipoServicios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimientoTipoServicio = await _context.ProcedimientoTipoServicio
                .Include(p => p.Procedimientos)
                .Include(p => p.TipoServicio)
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
                Text = "Tipos de servicio",
                Action = "Index",
                Controller = "TipoServicios",
                Area = "Parametricas",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = procedimientoTipoServicio.TipoServicio.Nombre,
                Action = "Details",
                Controller = "TipoServicios",
                Area = "Parametricas",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "id", procedimientoTipoServicio.TipoServicioId.ToString() }
                }
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Procedimientos",
                Action = "Index",
                Controller = "ProcedimientoTipoServicios",
                Area = "Administracion",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "tipoServicioId", procedimientoTipoServicio.TipoServicioId.ToString() }
                }
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Editar",

                Active = false

            });


            ViewBag.Breadcrumbs = breadcrumbList;


            if (procedimientoTipoServicio == null)
            {
                return NotFound();
            }
            
            var procedimientoEdit = new ProcedimientoCrear
            {
                Id = (Guid)id,
               
                TipoServicioId = procedimientoTipoServicio.TipoServicioId,
                ProcedimientoId = procedimientoTipoServicio.ProcedimientoId,
                Numero = procedimientoTipoServicio.Numero,
                Nombre = procedimientoTipoServicio.Procedimientos.Nombre,
                Codigo = procedimientoTipoServicio.Procedimientos.Codigo,
                Descripcion = procedimientoTipoServicio.Procedimientos.Descripcion,
                Estado = procedimientoTipoServicio.Procedimientos.Estado,
                TiempoEjecucion = procedimientoTipoServicio.Procedimientos.TiempoEjecucion
                
            };

            ViewData["ProcedimientoId"] = new SelectList(_context.Procedimiento, "Id", "Codigo", procedimientoTipoServicio.ProcedimientoId);
            ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Codigo", procedimientoTipoServicio.TipoServicioId);
            return View(procedimientoEdit);
        }

        // POST: Administracion/ProcedimientoTipoServicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Numero,TipoServicioId,Id,Nombre,Descripcion,Codigo,Estado,TiempoEjecucion,ProcedimientoId")] ProcedimientoCrear ProcedimientoEditar)
        {
            if (id != ProcedimientoEditar.Id)
            {
                return NotFound();
            }

            var procedimiento = new Procedimiento
            {
                Id = ProcedimientoEditar.ProcedimientoId,
                Nombre = ProcedimientoEditar.Nombre,
                Codigo = ProcedimientoEditar.Codigo,
                Descripcion = ProcedimientoEditar.Descripcion,
                Estado = ProcedimientoEditar.Estado,
                TiempoEjecucion = ProcedimientoEditar.TiempoEjecucion
            };
            var procedimientoTipoServicio = new ProcedimientoTipoServicio
            {
                Id = ProcedimientoEditar.Id,
                TipoServicioId = ProcedimientoEditar.TipoServicioId,
                ProcedimientoId = ProcedimientoEditar.ProcedimientoId,
                Numero = ProcedimientoEditar.Numero

            };
            if (ModelState.IsValid)
            {
         
                try
                {
                    _context.Update(procedimientoTipoServicio);
                    _context.Update(procedimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcedimientoTipoServicioExists(procedimientoTipoServicio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "ProcedimientoTipoServicios", new { area = "Administracion", tipoServicioId = procedimientoTipoServicio.TipoServicioId });
            }
        
            ViewData["TipoServicioId"] = new SelectList(_context.TipoServicio, "Id", "Codigo", procedimientoTipoServicio.TipoServicioId);
            return View(procedimientoTipoServicio);
        }

        // GET: Administracion/ProcedimientoTipoServicios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimientoTipoServicio = await _context.ProcedimientoTipoServicio
                .Include(p => p.Procedimientos)
                .Include(p => p.TipoServicio)
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
                Text = "Tipos de servicio",
                Action = "Index",
                Controller = "TipoServicios",
                Area = "Parametricas",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = procedimientoTipoServicio.TipoServicio.Nombre,
                Action = "Details",
                Controller = "TipoServicios",
                Area = "Parametricas",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "id", procedimientoTipoServicio.TipoServicioId.ToString() }
                }
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Procedimientos",
                Action = "Index",
                Controller = "ProcedimientoTipoServicios",
                Area = "Administracion",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "tipoServicioId", procedimientoTipoServicio.TipoServicioId.ToString() }
                }
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Eliminar",

                Active = false

            });


            ViewBag.Breadcrumbs = breadcrumbList;


            if (procedimientoTipoServicio == null)
            {
                return NotFound();
            }

            return View(procedimientoTipoServicio);
        }

        // POST: Administracion/ProcedimientoTipoServicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var procedimientoTipoServicio = await _context.ProcedimientoTipoServicio.FindAsync(id);
            _context.ProcedimientoTipoServicio.Remove(procedimientoTipoServicio);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "ProcedimientoTipoServicios", new { area = "Administracion", tipoServicioId = procedimientoTipoServicio.TipoServicioId });
        }

        private bool ProcedimientoTipoServicioExists(Guid id)
        {
            return _context.ProcedimientoTipoServicio.Any(e => e.Id == id);
        }
    }
}
