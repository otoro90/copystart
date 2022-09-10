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
using System.IO;

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
            ArchivoVM model = new ArchivoVM();
            return View(model);
        }

        // POST: Soportes/Archivos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArc(ArchivoVM archivovm)
        {
            
            var archivo = new Archivo();
            foreach(var a in archivovm.File) { 
            archivo.Tipo = a.FileName.Split(".").Last();
            archivo.Nombre = a.FileName.Substring(0, a.FileName.Length - (archivo.Tipo.Length+1));
            archivo.Peso = a.Length;
            if (ModelState.IsValid)
            {
                archivo.Id = Guid.NewGuid();
                _context.Add(archivo);

                var basePath = _configuration["PathBaseFiles"]+"/"+archivo.Id;

                using (var fileStream = System.IO.File.Create(basePath))
                {
                    await a.CopyToAsync(fileStream);
                }
                }
                await _context.SaveChangesAsync();
            }

                return RedirectToAction(nameof(Index));
        
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
            
            var basePath = _configuration["PathBaseFiles"] + "/" + archivo.Id;

            System.IO.File.Delete(basePath);
            _context.Archivo.Remove(archivo);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArchivoExists(Guid id)
        {
            return _context.Archivo.Any(e => e.Id == id);
        }


        public async Task<IActionResult> Download(Guid id)
        {
            var archivo = await _context.Archivo.FindAsync(id);

            var basePath = _configuration["PathBaseFiles"] + "/" + archivo.Id;

            string fileName = archivo.Nombre + "." + archivo.Tipo;


            byte[] bytes = System.IO.File.ReadAllBytes(basePath);
            

            return File(bytes, "application/octet-stream", fileName);
        }
    }
}
