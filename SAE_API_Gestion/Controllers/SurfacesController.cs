﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurfacesController : ControllerBase
    {
        private readonly IDataRepository<Surface> dataRepository;
        private readonly IDataRepositorySurface dataRepositorySurface;




        public SurfacesController(IDataRepository<Surface> dataRepo, IDataRepositorySurface dataRepositorySurface)
        {
            this.dataRepository = dataRepo;
            this.dataRepositorySurface = dataRepositorySurface;
        }

        // GET: api/Equipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Surface>>> GetSurfaces()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Equipements/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetSurfaceById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Surface>> GetSurfaceById(int id)
        {
            var surface = await dataRepository.GetByIdAsync(id);

            if (surface == null)
            {
                return NotFound();
            }

            return surface;
        }

        [HttpGet]
        [Route("[action]/{salleId}")]
        [ActionName("GetSurfacesBySalleId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]  // Réponse pour No Content
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Surface>>> GetSurfacesBySalleId(int salleId)
        {
            var surfaces = await dataRepositorySurface.GetSurfacesBySalleIdAsync(salleId);

            if (surfaces.Value == null || !surfaces.Value.Any())
            {
                return NoContent();  // Retourne un 204 si aucune surface n'est trouvée
            }

            return Ok(surfaces.Value);  // Retourne les surfaces si elles existent
        }



        // PUT: api/Equipements/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutSurface(int id, Surface surface)
        {
            if (id != surface.SurfaceId)
            {
                return BadRequest();
            }

            var surfaceToUpdate = await dataRepository.GetByIdAsync(id);

            if (surfaceToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(surfaceToUpdate.Value, surface);
                return NoContent();
            }
        }

        // POST: api/Equipements
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Surface>> PostSurface(Surface surface)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(surface);

            return CreatedAtAction("GetSurfaceById", new { id = surface.SurfaceId }, surface);
        }

        // DELETE: api/Equipements/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSurface(int id)
        {
            var surface = await dataRepository.GetByIdAsync(id);
            if (surface == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(surface.Value);
            return NoContent();
        }
    }
}
