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
    public class TypeSallesController : ControllerBase
    {
        private readonly IDataRepository<TypeSalle> dataRepository;



        public TypeSallesController(IDataRepository<TypeSalle> dataRepo)
        {
            this.dataRepository = dataRepo;
        }

        // GET: api/Equipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeSalle>>> GetTypeSalles()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Equipements/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetTypeSalleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeSalle>> GetTypeSalle(int id)
        {
            var salle = await dataRepository.GetByIdAsync(id);

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
        public async Task<IActionResult> PutTypeSalle(int id, TypeSalle salle)
        {
            if (id != salle.TypeSalleId)
            {
                return BadRequest();
            }

            var typeSalleToUpdate = await dataRepository.GetByIdAsync(id);

            if (typeSalleToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(typeSalleToUpdate.Value, salle);
                return NoContent();
            }
        }

        // POST: api/Equipements
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TypeSalle>> PostTypeSalle(TypeSalle salle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(salle);

            return CreatedAtAction("GetTypeSalleById", new { id = salle.TypeSalleId }, salle);
        }

        // DELETE: api/Equipements/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTypeSalle(int id)
        {
            var salle = await dataRepository.GetByIdAsync(id);
            if (salle == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(salle.Value);
            return NoContent();
        }
    }
}
