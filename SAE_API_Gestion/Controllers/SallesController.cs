using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SAE_API_Gestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SallesController : ControllerBase
    {
        private readonly IDataRepository<Salle> dataRepository;
        private readonly IDataRepositorySalle dataRepositorySalle;


        public SallesController(IDataRepository<Salle> dataRepo,IDataRepositorySalle datarepoSalle)
        {
            dataRepository = dataRepo;
            dataRepositorySalle = datarepoSalle;
        }

        // GET: api/Salles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salle>>> GetSalles()
        {
            return await dataRepository.GetAllAsync();
        }

        [HttpPost]
        [Route("[action]/{id}")]
        [ActionName("UploadImage")]
        public async Task<ActionResult<Salle>> UploadImage(int id, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var imageData = memoryStream.ToArray();

                    var salle = await dataRepository.GetByIdAsync(id);
                    if (salle == null)
                    {
                        return NotFound();
                    }

                    salle.Value.ImageData = imageData;
                    await dataRepository.UpdateAsync(salle.Value, salle.Value);
                    return NoContent();
                }
            }

            return BadRequest();
        }

        // GET: api/Salles/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetSalleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Salle>> GetSalleById(int id)
        {
            var salleResult = await dataRepository.GetByIdAsync(id);

            if (salleResult == null || salleResult.Value == null)
            {
                return NotFound();
            }

            var salle = salleResult.Value;

            // Construction d'un objet résultat avec le TypeSalle détaillé
            var result = new
            {
                salle.SalleId,
                salle.Nom,
                salle.Capacite,
                TypeSalle = new
                {
                    TypeSalleId = salle.TypeSalle?.TypeSalleId,
                    Nom = salle.TypeSalle?.Nom
                }
            };

            return Ok(result);
        }


        [HttpGet]
        [Route("[action]/{idsalle}")]
        [ActionName("GetSalleByBatiment")]
        public async Task<ActionResult<IEnumerable<Salle>>> GetAllSalleByBatiment(int idsalle)
        {
            return await dataRepositorySalle.GetAllSalleByBatimentAsync(idsalle);
        }

        [HttpGet("{str}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Salle>> GetSalleByString(string str)
        {
            var salle = await dataRepository.GetByStringAsync(str);

            if (salle == null)
            {
                return NotFound();
            }

            return salle;
        }

        // PUT: api/Salles/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutSalle(int id, Salle salle)
        {
            if (id != salle.SalleId)
            {
                return BadRequest();
            }

            var salleToUpdate = await dataRepository.GetByIdAsync(id);

            if (salleToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(salleToUpdate.Value, salle);
                return NoContent();
            }
        }

        // POST: api/Salles
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Salle>> PostSalle(Salle salle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(salle);

            return CreatedAtAction("GetSalleById", new { id = salle.SalleId }, salle);
        }

        // DELETE: api/Salles/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSalle(int id)
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
