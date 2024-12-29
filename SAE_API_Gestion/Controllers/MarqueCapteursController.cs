using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAE_API_Gestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarqueCapteursController : ControllerBase
    {
        private readonly IDataRepository<MarqueCapteur> dataRepository;

        public MarqueCapteursController(IDataRepository<MarqueCapteur> dataRepo)
        {
            this.dataRepository = dataRepo;
        }

        // GET: api/MarqueCapteurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarqueCapteur>>> GetMarqueCapteurs()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/MarqueCapteurs/5
        [HttpGet("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MarqueCapteur>> GetMarqueCapteurById(int id)
        {
            var marqueCapteur = await dataRepository.GetByIdAsync(id);

            if (marqueCapteur == null)
            {
                return NotFound();
            }

            return marqueCapteur;
        }

        // GET: api/MarqueCapteurs/{str}
        [HttpGet("{str}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MarqueCapteur>> GetMarqueCapteurByNom(string str)
        {
            var marqueCapteur = await dataRepository.GetByStringAsync(str);

            if (marqueCapteur == null)
            {
                return NotFound();
            }

            return marqueCapteur;
        }

        // PUT: api/MarqueCapteurs/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutMarqueCapteur(int id, MarqueCapteur marqueCapteur)
        {
            if (id != marqueCapteur.MarqueId)
            {
                return BadRequest();
            }

            var marqueCapteurToUpdate = await dataRepository.GetByIdAsync(id);

            if (marqueCapteurToUpdate == null)
            {
                return NotFound();
            }

            await dataRepository.UpdateAsync(marqueCapteurToUpdate.Value, marqueCapteur);
            return NoContent();
        }

        // POST: api/MarqueCapteurs
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MarqueCapteur>> PostMarqueCapteur(MarqueCapteur marqueCapteur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(marqueCapteur);

            return CreatedAtAction("GetMarqueCapteurById", new { id = marqueCapteur.MarqueId }, marqueCapteur);
        }

        // DELETE: api/MarqueCapteurs/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMarqueCapteur(int id)
        {
            var marqueCapteur = await dataRepository.GetByIdAsync(id);

            if (marqueCapteur == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(marqueCapteur.Value);
            return NoContent();
        }
    }
}
