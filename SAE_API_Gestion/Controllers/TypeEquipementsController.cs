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
    public class TypeEquipementsController : ControllerBase
    {
        private readonly IDataRepository<TypeEquipement> dataRepository;



        public TypeEquipementsController(IDataRepository<TypeEquipement> dataRepo)
        {
            this.dataRepository = dataRepo;
        }

        // GET: api/Equipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeEquipement>>> GetTypeEquipement()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Equipements/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetTypeEquipementById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeEquipement>> GetTypeEquipementById(int id)
        {
            var typeEquipement = await dataRepository.GetByIdAsync(id);

            if (typeEquipement == null)
            {
                return NotFound();
            }

            return typeEquipement;
        }


        // PUT: api/Equipements/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutTypeEquipement(int id, TypeEquipement typeEquipement)
        {
            if (id != typeEquipement.TypeEquipementId)
            {
                return BadRequest();
            }

            var typeEquipementToUpdate = await dataRepository.GetByIdAsync(id);

            if (typeEquipementToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(typeEquipementToUpdate.Value, typeEquipement);
                return NoContent();
            }
        }

        // POST: api/Equipements
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TypeEquipement>> PostTypeEquipement(TypeEquipement typeEquipement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(typeEquipement);

            return CreatedAtAction("GetTypeEquipementById", new { id = typeEquipement.TypeEquipementId }, typeEquipement);
        }

        // DELETE: api/Equipements/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTypeEquipement(int id)
        {
            var typeEquipement = await dataRepository.GetByIdAsync(id);
            if (typeEquipement == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(typeEquipement.Value);
            return NoContent();
        }
    }
}
