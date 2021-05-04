using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities;
using DAL.Repositories.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesServicioController : ControllerBase
    {

        private readonly ICRUDRepository<SolicitudServicio, int> _solicitudServicioRepository;

        public SolicitudesServicioController(ICRUDRepository<SolicitudServicio, int> solicitudServicioRepository)
        {
            _solicitudServicioRepository = solicitudServicioRepository;
        }

        /// <summary>
        /// Metodo api que obtiene el listado de solicitudServicioes
        /// </summary>
        /// <returns>listado de solicitudServicioes</returns>
        /// <response code="200">Resultado de transacción</response>
        /// <response code="204">Sin información</response>
        /// <response code="500">Ha ocurrido un error</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolicitudServicio>>> Get()
        {
            try
            {
                var list = await _solicitudServicioRepository.Get();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Metodo api que obtiene la solicitudServicio por medio de su id
        /// </summary>
        /// <returns>objeto de solicitudServicio</returns>
        /// <response code="200">Resultado de transacción</response>
        /// <response code="204">Sin información</response>
        /// <response code="500">Ha ocurrido un error</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<SolicitudServicio>> Get(int id)
        {
            try
            {
                var obj = await _solicitudServicioRepository.Get(id);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Metodo api que actualiza solicitudServicio
        /// </summary>
        /// <returns>Resultado de transacción</returns>
        /// <response code="200">Resultado de transacción</response>
        /// <response code="204">Sin información</response>
        /// <response code="500">Ha ocurrido un error</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SolicitudServicio solicitudServicio)
        {
            if (id != solicitudServicio.Id)
            {
                return BadRequest("Identificadores de objeto en body y de url no coinciden");
            }
           
            try
            {
                await _solicitudServicioRepository.Update(solicitudServicio);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await _solicitudServicioRepository.Exist(solicitudServicio)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Metodo api que crea solicitudServicio
        /// </summary>
        /// <returns>Resultado de transacción</returns>
        /// <response code="200">Resultado de transacción</response>
        /// <response code="204">Sin información</response>
        /// <response code="500">Ha ocurrido un error</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<SolicitudServicio>> Create(SolicitudServicio solicitudServicio)
        {

            try
            {
                var obj = await _solicitudServicioRepository.Create(solicitudServicio);

                return CreatedAtAction("Get", new { id = obj.Id }, solicitudServicio);

            }
            catch (DbUpdateException)
            {
                if (await _solicitudServicioRepository.Exist(solicitudServicio))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

        }

        /// <summary>
        /// Metodo api que elimina solicitudServicio
        /// </summary>
        /// <returns>Resultado de transacción</returns>
        /// <response code="200">Resultado de transacción</response>
        /// <response code="204">Sin información</response>
        /// <response code="500">Ha ocurrido un error</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var solicitudServicio = await _solicitudServicioRepository.Get(id);
            if (solicitudServicio == null)
            {
                return NotFound();
            }

            await _solicitudServicioRepository.Delete(solicitudServicio); 

            return NoContent();
        }
    }
}
