using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CopyStart.Data;
using CopyStart.Entities;
using CopyStart.Filters;
using CopyStart.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CopyStart.Areas.Parametricas.Controllers
{
    [Area("Parametricas")]
    public class UbicacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UbicacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parametricas/Ubicaciones
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
                Text = "Ubicaciones",
                Action = "Index",
                Controller = "Ubicaciones",
                Area = "Parametricas",
                Active = true
            });
            ViewBag.Breadcrumbs = breadcrumbList;


            return View(await _context.Ubicacion.ToListAsync());
        }

        // GET: Parametricas/Ubicaciones/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ubicacion = await _context.Ubicacion
                .FirstOrDefaultAsync(m => m.CodigoMunicipio == id);


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
                Text = "Ubicaciones",
                Action = "Index",
                Controller = "Ubicaciones",
                Area = "Parametricas",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = ubicacion.Municipio,
                Action = "Details",
                Controller = "Ubicaciones",
                Area = "Parametricas",
                Active = false,
                Params = new Dictionary<string, string>
                {
                    { "id", id.ToString() }
                }
            });
            ViewBag.Breadcrumbs = breadcrumbList;


            if (ubicacion == null)
            {
                return NotFound();
            }

            return View(ubicacion);
        }

        // GET: Parametricas/Ubicaciones/Create
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
                Text = "Ubicaciones",
                Action = "Index",
                Controller = "Ubicaciones",
                Area = "Parametricas",
                Active = true
            });
           
            breadcrumbList.Add(new Breadcrumb
            {
                Text = "Crear",
                Active = false

            }); ViewBag.Breadcrumbs = breadcrumbList;


            return View();
        }

        // POST: Parametricas/Ubicaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoMunicipio,Municipio,CodigoDepartamento,Departamento,Latitud,Longitud")] Ubicacion ubicacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ubicacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ubicacion);
        }

        // GET: Parametricas/Ubicaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ubicacion = await _context.Ubicacion.FindAsync(id);

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
                Text = "Ubicaciones",
                Action = "Index",
                Controller = "Ubicaciones",
                Area = "Parametricas",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = ubicacion.Municipio,
                Action = "Details",
                Controller = "Ubicaciones",
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

            if (ubicacion == null)
            {
                return NotFound();
            }
            return View(ubicacion);
        }

        // POST: Parametricas/Ubicaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodigoMunicipio,Municipio,CodigoDepartamento,Departamento,Latitud,Longitud")] Ubicacion ubicacion)
        {
            if (id != ubicacion.CodigoMunicipio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ubicacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UbicacionExists(ubicacion.CodigoMunicipio))
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
            return View(ubicacion);
        }

        // GET: Parametricas/Ubicaciones/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ubicacion = await _context.Ubicacion
                .FirstOrDefaultAsync(m => m.CodigoMunicipio == id);

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
                Text = "Ubicaciones",
                Action = "Index",
                Controller = "Ubicaciones",
                Area = "Parametricas",
                Active = true
            });
            breadcrumbList.Add(new Breadcrumb
            {
                Text = ubicacion.Municipio,
                Action = "Details",
                Controller = "Ubicaciones",
                Area = "Parametricas",
                Active = true,
                Params = new Dictionary<string, string>
                {
                    { "id", id.ToString() }
                }
            }); breadcrumbList.Add(new Breadcrumb
            {
                Text = "Eliminar",

                Active = false,

            });

            ViewBag.Breadcrumbs = breadcrumbList;

            if (ubicacion == null)
            {
                return NotFound();
            }

            return View(ubicacion);
        }

        // POST: Parametricas/Ubicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ubicacion = await _context.Ubicacion.FindAsync(id);
            _context.Ubicacion.Remove(ubicacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UbicacionExists(string id)
        {
            return _context.Ubicacion.Any(e => e.CodigoMunicipio == id);
        }

        // GET: Parametricas/Ubicaciones
        public async Task<IActionResult> GetDepartamentos()
        {
            return Json(new SelectList(await _context.Ubicacion.Select(x=> new { Codigo = x.CodigoDepartamento, Departamento = x.Departamento  }).ToListAsync(), "Codigo", "Departamento"));
        }

        // GET: Parametricas/Ubicaciones
        public async Task<IActionResult> GetMunicipios(string codigoDepartamento)
        {
            return Json(new SelectList(await _context.Ubicacion.Where(x => string.IsNullOrEmpty(codigoDepartamento) || x.CodigoDepartamento == codigoDepartamento).ToListAsync(), "CodigoMunicipio", "Municipio"));
        }
    }
}
