using DAL.Repositories.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

#nullable disable

namespace DAL.Repositories
{
    public partial class SolicitudServicioRepository : ICRUDRepository<SolicitudServicio,int>
    {
        private readonly copystartdbContext _context;

        public SolicitudServicioRepository(copystartdbContext context)
        {
            _context = context;
        }

        public async Task<SolicitudServicio> Create(SolicitudServicio entity)
        {
            _context.SolicitudesServicio.Add(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

        public async Task Delete(SolicitudServicio entity)
        {

            _context.SolicitudesServicio.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SolicitudServicio>> Get()
        {
            return await _context.SolicitudesServicio.ToListAsync();
        }

        public async Task<SolicitudServicio> Get(int id)
        {
            var clasificacion = await _context.SolicitudesServicio.FindAsync(id);

            return clasificacion;
        }

        public async Task Update(SolicitudServicio entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

        }

        public async Task<bool> Exist(SolicitudServicio entity)
        {
            return await _context.SolicitudesServicio.AnyAsync(e => e.Id == entity.Id);

        }
    }
}
