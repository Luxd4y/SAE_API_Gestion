using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipementController : ControllerBase
{
    private readonly IDataRepository<Equipement> _equipementManager;

    public EquipementController(IDataRepository<Equipement> equipementManager)
    {
        _equipementManager = equipementManager;
    }


    // GET: api/Equipement
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Equipement>>> GetEquipements()
    {
        return await _equipementManager.GetAllAsync();
    }


    // GET: api/Equipement/5
    [HttpGet("[action]/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Equipement>> GetEquipementById(int id)
    {
        var equipement = await _equipementManager.GetByIdAsync(id);

        if (equipement.Result is NotFoundResult)
        {
            return NotFound();
        }

        return equipement.Value;
    }


    // GET: api/Equipement/{str}
    [HttpGet("{str}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Equipement>> GetEquipementByName(string str)
    {
        var equipement = await _equipementManager.GetByStringAsync(str);

        if (equipement.Result is NotFoundResult)
        {
            return NotFound();
        }

        return equipement.Value;
    }


    // POST: api/Equipement
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostEquipement(Equipement equipement)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await _equipementManager.AddAsync(equipement);
        return CreatedAtAction("GetEquipementById", new { id = equipement.EquipementId }, equipement);

    }


    // PUT: api/Equipement/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutEquipement(int id, Equipement equipement)
    {
        if (id != equipement.EquipementId)
        {
            return BadRequest();
        }

        var existingEquipement = await _equipementManager.GetByIdAsync(id);

        if (existingEquipement.Result is NotFoundResult)
        {
            return NotFound();
        }

        await _equipementManager.UpdateAsync(existingEquipement.Value, equipement);

        return NoContent();
    }


    // DELETE: api/Equipement/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEquipement(int id)
    {
        var equipement = await _equipementManager.GetByIdAsync(id);

        if (equipement.Result is NotFoundResult)
        {
            return NotFound();
        }

        await _equipementManager.DeleteAsync(equipement.Value);

        return NoContent();
    }
}
