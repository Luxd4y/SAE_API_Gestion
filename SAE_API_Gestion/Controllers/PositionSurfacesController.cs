using System;
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
    public class PositionSurfacesController : ControllerBase
    {
        private readonly IDataRepository<PositionSurface> dataRepository;



        public PositionSurfacesController(IDataRepository<PositionSurface> dataRepo)
        {
            this.dataRepository = dataRepo;
        }

        // GET: api/Equipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionSurface>>> GetPositionSurfaces()
        {
            return await dataRepository.GetAllAsync();
        }




        // GET: api/Equipements/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetPositionSurfaceById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PositionSurface>> GetMarqueCapteurById(int id)
        {
            var positionSurface = await dataRepository.GetByIdAsync(id);

            if (positionSurface == null)
            {
                return NotFound();
            }

            return positionSurface;
        }
        [HttpGet("{str}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PositionSurface>> GetPositionSurfaceByString(string str)
        {
            var positionSurface = await dataRepository.GetByStringAsync(str);

            if (positionSurface == null)
            {
                return NotFound();
            }

            return positionSurface;
        }
        // PUT: api/Equipements/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutPositionSurface(int id, PositionSurface positionSurface)
        {
            if (id != positionSurface.PositionSurfaceId)
            {
                return BadRequest();
            }

            var postionSurfaceToUpdate = await dataRepository.GetByIdAsync(id);

            if (postionSurfaceToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(postionSurfaceToUpdate.Value, positionSurface);
                return NoContent();
            }
        }

        // POST: api/Equipements
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PositionSurface>> PostPositionSurface(PositionSurface positionSurface)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(positionSurface);

            return CreatedAtAction("GetPositionSurfaceById", new { id = positionSurface.PositionSurfaceId }, positionSurface);
        }

        // DELETE: api/Equipements/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePositionSurface(int id)
        {
            var positionSurface = await dataRepository.GetByIdAsync(id);
            if (positionSurface == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(positionSurface.Value);
            return NoContent();
        }
    }
}
