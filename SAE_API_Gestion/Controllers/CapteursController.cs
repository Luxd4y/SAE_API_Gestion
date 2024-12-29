using Microsoft.AspNetCore.Mvc;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CapteursController : ControllerBase
{
    private readonly IDataRepository<Capteur> _capteurManager;

    public CapteursController(IDataRepository<Capteur> capteurManager)
    {
        _capteurManager = capteurManager;
    }


    // GET: api/Capteurs
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Capteur>>> GetCapteurs()
    {
        return await _capteurManager.GetAllAsync();
    }


    // GET: api/Capteurs/GetById/5
    [HttpGet("[action]/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Capteur>> GetCapteurById(int id)
    {
        var capteur = await _capteurManager.GetByIdAsync(id);

        if (capteur.Result is NotFoundResult)
        {
            return NotFound();
        }

        return capteur.Value;
    }


    // GET: api/Capteurs/{str}
    [HttpGet("{str}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Capteur>> GetCapteurByNom(string str)
    {
        var capteur = await _capteurManager.GetByStringAsync(str);

        if (capteur.Result is NotFoundResult)
        {
            return NotFound();
        }

        return capteur.Value;
    }


    // POST: api/Capteurs
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostCapteur(Capteur capteur)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _capteurManager.AddAsync(capteur);

        return CreatedAtAction("GetById", new { id = capteur.CapteurId }, capteur);
    }



    // PUT: api/Capteurs/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutCapteur(int id, Capteur capteur)
    {
        if (id != capteur.CapteurId)
        {
            return BadRequest();
        }

        var existingCapteur = await _capteurManager.GetByIdAsync(id);

        if (existingCapteur.Result is NotFoundResult)
        {
            return NotFound();
        }

        await _capteurManager.UpdateAsync(existingCapteur.Value, capteur);

        return NoContent();
    }



    // DELETE: api/Capteurs/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCapteur(int id)
    {
        var capteur = await _capteurManager.GetByIdAsync(id);

        if (capteur.Result is NotFoundResult)
        {
            return NotFound();
        }

        await _capteurManager.DeleteAsync(capteur.Value);

        return NoContent();
    }
}
