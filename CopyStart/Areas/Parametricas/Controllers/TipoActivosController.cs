using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CopyStart.Data;
using CopyStart.Entities;
using Microsoft.AspNetCore.Authorization;
using CopyStart.Filters;
using CopyStart.Models;

namespace CopyStart.Areas.Parametricas.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Area("Parametricas")]
    public class TipoActivosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoActivosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parametricas/TipoActivos
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
                Text = "Tipos de activo",
                Action = "Index",
                Controller = "TipoActivos",
                Area = "Parametricas",
                Active = false
            });
            ViewBag.Breadcrumbs = breadcrumbList;

            return View(await _context.TipoActivo.ToListAsync());
        }

        // GET: Parametricas/TipoActivos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoActivo = await _context.TipoActivo
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
                Text = "Tipos de activo",
                Action = "Index",
                Controller = "TipoActivos",
                Area = "Parametricas",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = tipoActivo.Nombre,
                Action = "Details",
                Controller = "TipoActivos",
                Area = "Parametricas",
                Active = false,
                Params = new Dictionary<string, string>
                {
                    { "id", id.ToString() }
                }
            }); ViewBag.Breadcrumbs = breadcrumbList;

            if (tipoActivo == null)
            {
                return NotFound();
            }

            return View(tipoActivo);
        }

        // GET: Parametricas/TipoActivos/Create
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
                Text = "Tipos de activo",
                Action = "Index",
                Controller = "TipoActivos",
                Area = "Parametricas",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Crear",
                
                Active = false,
                
            });
            ViewBag.Breadcrumbs = breadcrumbList;

            return View();
        }

        // POST: Parametricas/TipoActivos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Codigo,Estado")] TipoActivo tipoActivo)
        {
            if (ModelState.IsValid)
            {
                tipoActivo.Id = Guid.NewGuid();
                _context.Add(tipoActivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoActivo);
        }

        // GET: Parametricas/TipoActivos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoActivo = await _context.TipoActivo.FindAsync(id);

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
                Text = "Tipo de activo",
                Action = "Index",
                Controller = "TipoActivos",
                Area = "Parametricas",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = tipoActivo.Nombre,
                Action = "Details",
                Controller = "TipoActivos",
                Area = "Parametricas",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "id", id.ToString() }
                }
            }); breadcrumbList.Add(new Breadcrumb
            {
                Text = "Editar",

                Active = false,

            });
            ViewBag.Breadcrumbs = breadcrumbList;
            if (tipoActivo == null)
            {
                return NotFound();
            }
            return View(tipoActivo);
        }

        // POST: Parametricas/TipoActivos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,Descripcion,Codigo,Estado")] TipoActivo tipoActivo)
        {
            if (id != tipoActivo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoActivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoActivoExists(tipoActivo.Id))
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
            return View(tipoActivo);
        }

        // GET: Parametricas/TipoActivos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoActivo = await _context.TipoActivo
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
                Text = "Tipos de activo",
                Action = "Index",
                Controller = "TipoActivos",
                Area = "Parametricas",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = tipoActivo.Nombre,
                Action = "Details",
                Controller = "TipoActivos",
                Area = "Parametricas",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "id", id.ToString() }
                }
            }); breadcrumbList.Add(new Breadcrumb
            {
                Text = "Borrar",

                Active = false,

            });
            ViewBag.Breadcrumbs = breadcrumbList;
            if (tipoActivo == null)
            {
                return NotFound();
            }

            return View(tipoActivo);
        }

        // POST: Parametricas/TipoActivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tipoActivo = await _context.TipoActivo.FindAsync(id);
            _context.TipoActivo.Remove(tipoActivo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoActivoExists(Guid id)
        {
            return _context.TipoActivo.Any(e => e.Id == id);
        }
    }
}
