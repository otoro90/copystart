using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CopyStart.Data;
using CopyStart.Entities;
using Microsoft.Extensions.Configuration;

namespace CopyStart.Areas.Tecnica.Controllers
{
    [Area("Tecnica")]
    public class ArchivoServiciosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ArchivoServiciosController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Tecnica/ArchivoServicios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ArchivoServicio.Include(a => a.Archivo).Include(a => a.Servicio);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tecnica/ArchivoServicios/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var archivoServicio = await _context.ArchivoServicio
                .Include(a => a.Archivo)
                .Include(a => a.Servicio)
                .FirstOrDefaultAsync(m => m.ServicioId == id);
            if (archivoServicio == null)
            {
                return NotFound();
            }

            return View(archivoServicio);
        }

        // GET: Tecnica/ArchivoServicios/Create
        public IActionResult Create()
        {
            ViewData["ArchivoId"] = new SelectList(_context.Archivo, "Id", "Nombre");
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Id", "Id");
            return View();
        }

        // POST: Tecnica/ArchivoServicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArchivoId,ServicioId")] ArchivoServicio archivoServicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(archivoServicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArchivoId"] = new SelectList(_context.Archivo, "Id", "Nombre", archivoServicio.ArchivoId);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Id", "Id", archivoServicio.ServicioId);
            return View(archivoServicio);
        }

        // GET: Tecnica/ArchivoServicios/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var archivoServicio = await _context.ArchivoServicio.FindAsync(id);
            if (archivoServicio == null)
            {
                return NotFound();
            }
            ViewData["ArchivoId"] = new SelectList(_context.Archivo, "Id", "Nombre", archivoServicio.ArchivoId);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Id", "Id", archivoServicio.ServicioId);
            return View(archivoServicio);
        }

        // POST: Tecnica/ArchivoServicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ArchivoId,ServicioId")] ArchivoServicio archivoServicio)
        {
            if (id != archivoServicio.ServicioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(archivoServicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArchivoServicioExists(archivoServicio.ServicioId))
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
            ViewData["ArchivoId"] = new SelectList(_context.Archivo, "Id", "Nombre", archivoServicio.ArchivoId);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "Id", "Id", archivoServicio.ServicioId);
            return View(archivoServicio);
        }

        // GET: Tecnica/ArchivoServicios/Delete/5
       

        // POST: Tecnica/ArchivoServicios/Delete/5
        
       
        public async Task<IActionResult> Delete(long id)
        {
            var archivoServicio = await _context.ArchivoServicio.FindAsync(id);
            _context.ArchivoServicio.Remove(archivoServicio);
            await _context.SaveChangesAsync();
            var archivo = await _context.Archivo.FindAsync(id);

            var basePath = _configuration["PathBaseFiles"] + "/" + archivo.Id;

            System.IO.File.Delete(basePath);
            _context.Archivo.Remove(archivo);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArchivoServicioExists(long id)
        {
            return _context.ArchivoServicio.Any(e => e.ServicioId == id);
        }
    }
}
