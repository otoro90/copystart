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

namespace CopyStart.Areas.Activos.Controllers
{
    [Area("Activos")]
    public class ActivosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ActivosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Activos/Activos
        [Authorize(Roles = "Administrador, Coordinador, Tecnico, Cliente")]
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
                Text = "Activos",
                Action = "Index",
                Controller = "Activos",
                Area = "Tecnica",
                Active = false
            });
        ViewBag.Breadcrumbs = breadcrumbList;

            List<Activo> listaActivos = null;
            
            
                if (User.IsInRole("Administrador") || User.IsInRole("Coordinador"))
                {

                    listaActivos = await _context.Activo.Include(a => a.Persona).Include(a => a.TipoActivo).Include(a => a.MarcaActivo).
                    Include(a => a.ModeloActivo).Where(x => x.Estado != "Eliminado").ToListAsync();


                }
                if (User.IsInRole("Cliente"))
                {
                    var user = await _userManager.GetUserAsync(User);
                    listaActivos = await _context.Activo.Include(a => a.Persona).Include(a => a.TipoActivo).Include(a => a.MarcaActivo).
                    Include(a => a.ModeloActivo).Where(x => x.PersonaId == user.PersonaId && x.Estado != "Eliminado").ToListAsync();

                }

            return View(listaActivos);
        }

        // GET: Activos/Activos/Details/5
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
                Text = "Activos",
                Action = "Index",
                Controller = "Activos",
                Area = "Activos",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Activo " + id.ToString(),
                Action = "Details",
                Controller = "Activos",
                Area = "Activos",
                Active = false,
                Params = new Dictionary<string, string>
                {
                    { "id", id.ToString() }
                }
            });
           
            ViewBag.Breadcrumbs = breadcrumbList;








            if (id == null)
            {
                return NotFound();
            }
            var solicitudes = _context.Solicitud.Include(s => s.Activo).ThenInclude(s => s.MarcaActivo).Include(s => s.Cliente).Include(s => s.Tecnico).Where(s => s.ActivoId == id);
            var activo = await _context.Activo
                .Include(a => a.Persona)
                .Include(a => a.TipoActivo)
                .Include(a => a.MarcaActivo)
                .Include(a => a.ModeloActivo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activo == null)
            {
                return NotFound();
            }
            if (solicitudes == null)
            {

            }
            

            return View(activo);
        }

        // GET: Activos/Activos/Create
        [Authorize(Roles = "Administrador, Cliente")]
        public IActionResult Create()
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
                Text = "Activos",
                Action = "Index",
                Controller = "Activos",
                Area = "Activos",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Crear ",             
                Active = false,
               
            });
           
            ViewBag.Breadcrumbs = breadcrumbList;






            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id");
            ViewData["TipoActivoId"] = new SelectList(_context.Set<TipoActivo>(), "Id", "Nombre");
            ViewData["MarcaActivoId"] = new SelectList(_context.Set<MarcaActivo>(), "Id", "Nombre");
            ViewData["ModeloActivoId"] = new SelectList(_context.Set<ModeloActivo>(), "Id", "Nombre");
            return View();
        }

        // POST: Activos/Activos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Create([Bind("Id,Serial,TipoActivoId,Descripcion,MarcaActivoId,ModeloActivoId,FechaRegistro,PersonaId")] Activo activo)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                
               
                activo.FechaRegistro = DateTime.Now;
                activo.PersonaId = (Guid)user.PersonaId;
                activo.Estado = "Inactivo";
                _context.Add(activo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Activos", new { area = "Activos", id = activo.Id });
            }

            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", activo.PersonaId);
            ViewData["TipoActivoId"] = new SelectList(_context.Set<TipoActivo>(), "Id", "Codigo", activo.TipoActivoId);
            ViewData["MarcaActivoId"] = new SelectList(_context.Set<MarcaActivo>(), "Id", "Nombre", activo.MarcaActivoId);
            ViewData["ModeloActivoId"] = new SelectList(_context.Set<ModeloActivo>(), "Id", "Nombre", activo.ModeloActivoId);
            return View(activo);
        }

        // GET: Activos/Activos/Edit/5
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Edit(long? id)
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
                Text = "Activos",
                Action = "Index",
                Controller = "Activos",
                Area = "Activos",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Editar activo " + id.ToString(),              
                Active = true,
                
            });
           
            ViewBag.Breadcrumbs = breadcrumbList;


            if (id == null)
            {
                return NotFound();
            }

            var activo = await _context.Activo.FindAsync(id);
            if (activo == null)
            {
                return NotFound();
            }
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", activo.PersonaId);
            ViewData["TipoActivoId"] = new SelectList(_context.Set<TipoActivo>(), "Id", "Codigo", activo.TipoActivoId);
            ViewData["MarcaActivoId"] = new SelectList(_context.Set<MarcaActivo>(), "Id", "Nombre", activo.MarcaActivoId);
            ViewData["ModeloActivoId"] = new SelectList(_context.Set<ModeloActivo>(), "Id", "Nombre", activo.ModeloActivoId);
            ViewData["Serial"] = activo.Serial;
            ViewData["Estado"] = activo.Estado;

            return View(activo);
        }

        // POST: Activos/Activos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Serial,TipoActivoId,Descripcion,MarcaActivoId,ModeloActivoId,FechaRegistro,PersonaId,Estado")] Activo activo)
        {
            if (id != activo.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivoExists(activo.Id))
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
            ViewData["PersonaId"] = new SelectList(_context.Persona, "Id", "Id", activo.PersonaId);
            ViewData["TipoActivoId"] = new SelectList(_context.Set<TipoActivo>(), "Id", "Codigo", activo.TipoActivoId);
            ViewData["MarcaActivoId"] = new SelectList(_context.Set<MarcaActivo>(), "Id", "Nombre", activo.MarcaActivoId);
            ViewData["ModeloActivoId"] = new SelectList(_context.Set<ModeloActivo>(), "Id", "Nombre", activo.ModeloActivoId);
            return View(activo);
        }

        // GET: Activos/Activos/Delete/5
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activo = await _context.Activo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activo == null)
            {
                return NotFound();
            }

            return View(activo);
        }

        // POST: Activos/Activos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var activo = await _context.Activo.FindAsync(id);
            _context.Activo.Remove(activo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivoExists(long id)
        {
            return _context.Activo.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ActivosPorCliente(Guid? idCliente)
        {
           
            var applicationDbContext = _context.Activo.Include(a => a.Persona).Include(a => a.TipoActivo).Where(x => x.PersonaId == idCliente && x.Estado!="Eliminado");
            return View(await applicationDbContext.ToListAsync());
        }


        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> Eliminar(long? id)
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
                Text = "Activos",
                Action = "Index",
                Controller = "Activos",
                Area = "Activos",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Eliminar activo " + id.ToString(),            
                Active= false
                
            });
            
            ViewBag.Breadcrumbs = breadcrumbList;

            if (id == null)
            {
                return NotFound();
            }

            var activo = await _context.Activo.Include(m => m.TipoActivo).Include(m => m.MarcaActivo).Include(m => m.ModeloActivo).Include(m => m.Persona)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activo == null)
            {
                return NotFound();
            }

            return View(activo);
        }

        // POST: Activos/Activos/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> EliminarConfirmado(long id)
        {
            var activo = await _context.Activo.FindAsync(id);
            activo.Estado = "Eliminado";
            _context.Update(activo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



    }
}
