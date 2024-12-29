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
    public class ParametreCapteursController : ControllerBase
    {
        private readonly IDataRepository<ParametreCapteur> dataRepository;
        private readonly IDataRepositoryParametreCapteur dataRepositoryParametreCapteur;




        public ParametreCapteursController(IDataRepository<ParametreCapteur> dataRepo)
        {
            this.dataRepository = dataRepo;
        }

        // GET: api/Equipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParametreCapteur>>> GetMarqueCapteurs()
        {
            return await dataRepository.GetAllAsync();
        }



        // GET: api/Equipements/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetParametreCapteurById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ParametreCapteur>> GetParametreCapteurById(int unitemesureid,int capteurid)
        {
            var parametreCapteur = await dataRepositoryParametreCapteur.GetJoinByDoubleIdAsync(unitemesureid,capteurid);

            if (parametreCapteur == null)
            {
                return NotFound();
            }

            return parametreCapteur;
        }
        [HttpGet("{str}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ParametreCapteur>> GetParametreCapteurByString(string str)
        {
            var parametreCapteur = await dataRepository.GetByStringAsync(str);

            if (parametreCapteur == null)
            {
                return NotFound();
            }

            return parametreCapteur;
        }
        // PUT: api/Equipements/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutParametreCapteur(int unitemesureid,int idcapteur, ParametreCapteur parametreCapteur)
        {
            if (unitemesureid != parametreCapteur.UniteMesurerId || idcapteur!=parametreCapteur.CapteurId)
            {
                return BadRequest();
            }

            var parametreCapteurToUpdate = await dataRepositoryParametreCapteur.GetJoinByDoubleIdAsync(unitemesureid,idcapteur);

            if (parametreCapteurToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await dataRepository.UpdateAsync(parametreCapteurToUpdate.Value, parametreCapteur);
                return NoContent();
            }
        }

        // POST: api/Equipements
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ParametreCapteur>> PostParametreCapteur(ParametreCapteur parametreCapteur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(parametreCapteur);

            return CreatedAtAction("GetParametreCapteurById", new { id = parametreCapteur.UniteMesurerId,id2=parametreCapteur.CapteurId}, parametreCapteur);
        }

        // DELETE: api/Equipements/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteParametreCapteur(int id)
        {
            var parametreCapteur = await dataRepository.GetByIdAsync(id);
            if (parametreCapteur == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(parametreCapteur.Value);
            return NoContent();
        }
    }
}
