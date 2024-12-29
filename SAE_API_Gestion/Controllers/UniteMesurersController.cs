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
    public class UniteMesurersController : ControllerBase
    {
        private readonly IDataRepository<UniteMesurer> dataRepository;



        public UniteMesurersController(IDataRepository<UniteMesurer> dataRepo)
        {
            this.dataRepository = dataRepo;
        }

        // GET: api/Equipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UniteMesurer>>> GetTypeSalles()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Equipements/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetUniteMesurerById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UniteMesurer>> GetTypeSalle(int id)
        {
            var uniteMesurer = await dataRepository.GetByIdAsync(id);

            if (uniteMesurer == null)
            {
                return NotFound();
            }

            return uniteMesurer;
        }

        [HttpGet("{str}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UniteMesurer>> GetUniteMesurerByString(string str)
        {
            var salle = await dataRepository.GetByStringAsync(str);

            if (salle == null)
            {
                return NotFound();
            }

            return salle;
        }



        // PUT: api/Equipements/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutTypeSalle(int id, UniteMesurer uniteMesurer)
        {
            if (id != uniteMesurer.UniteMesurerId)
            {
                return BadRequest();
            }

            var uniteMesureToUpdate = await dataRepository.GetByIdAsync(id);

            if (uniteMesureToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(uniteMesureToUpdate.Value, uniteMesurer);
                return NoContent();
            }
        }

        // POST: api/Equipements
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UniteMesurer>> PostTypeSalle(UniteMesurer uniteMesurer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(uniteMesurer);

            return CreatedAtAction("GetUniteMesurerById", new { id = uniteMesurer.UniteMesurerId }, uniteMesurer);
        }

        // DELETE: api/Equipements/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTypeSalle(int id)
        {
            var uniteMesurer = await dataRepository.GetByIdAsync(id);
            if (uniteMesurer == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(uniteMesurer.Value);
            return NoContent();
        }
    }
}
