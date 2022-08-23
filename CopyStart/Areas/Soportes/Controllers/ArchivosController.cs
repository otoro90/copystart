using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CopyStart.Data;
using CopyStart.Entities;
using Microsoft.AspNetCore.Http;
using CopyStart.Areas.Soportes.Models;
using Microsoft.Extensions.Configuration;

namespace CopyStart.Areas.Soportes.Controllers
{
    [Area("Soportes")]
    public class ArchivosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ArchivosController(ApplicationDbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Soportes/Archivos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Archivo.ToListAsync());
        }

        // GET: Soportes/Archivos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var archivo = await _context.Archivo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (archivo == null)
            {
                return NotFound();
            }

            return View(archivo);
        }

        // GET: Soportes/Archivos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Soportes/Archivos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("File")] ArchivoVM archivoVM)
        {
            var archivo = new Archivo();
            archivo.Tipo = archivoVM.File.FileName.Split(".").Last();
            archivo.Nombre = archivoVM.File.FileName.Substring(0, archivoVM.File.FileName.Length - (archivo.Tipo.Length+1));
            archivo.Peso = archivoVM.File.Length;
            if (ModelState.IsValid)
            {
                archivo.Id = Guid.NewGuid();
                _context.Add(archivo);

                var basePath = _configuration["PathBaseFiles"];

                using (var fileStream = System.IO.File.Create(basePath))
                {
                    await archivoVM.File.CopyToAsync(fileStream);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(archivoVM);
        }

        // GET: Soportes/Archivos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var archivo = await _context.Archivo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (archivo == null)
            {
                return NotFound();
            }

            return View(archivo);
        }

        // POST: Soportes/Archivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var archivo = await _context.Archivo.FindAsync(id);
            _context.Archivo.Remove(archivo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArchivoExists(Guid id)
        {
            return _context.Archivo.Any(e => e.Id == id);
        }
    }
}
