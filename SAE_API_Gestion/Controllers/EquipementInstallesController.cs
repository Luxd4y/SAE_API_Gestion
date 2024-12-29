using Microsoft.AspNetCore.Mvc;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipementInstallesController : ControllerBase
{
    private readonly IDataRepository<EquipementInstalle> _equipementInstalleManager;

    public EquipementInstallesController(IDataRepository<EquipementInstalle> equipementInstalleManager)
    {
        _equipementInstalleManager = equipementInstalleManager;
    }


    // GET: api/EquipementInstalle
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EquipementInstalle>>> GetEquipementInstalles()
    {
        return await _equipementInstalleManager.GetAllAsync();
    }


    // GET: api/EquipementInstalle/5
    [HttpGet("[action]/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EquipementInstalle>> GetEquipementInstalleById(int id)
    {
        var equipementInstalle = await _equipementInstalleManager.GetByIdAsync(id);

        if (equipementInstalle == null || equipementInstalle.Result is NotFoundResult)
        {
            return NotFound();
        }

        return equipementInstalle.Value;
    }


    // POST: api/EquipementInstalle
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostEquipementInstalle(EquipementInstalle equipementInstalle)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _equipementInstalleManager.AddAsync(equipementInstalle);

        return CreatedAtAction(nameof(GetEquipementInstalleById), new { id = equipementInstalle.EquipementInstalleId }, equipementInstalle);
    }


    // PUT: api/EquipementInstalle/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutEquipementInstalle(int id, EquipementInstalle equipementInstalle)
    {
        if (id != equipementInstalle.EquipementInstalleId)
        {
            return BadRequest();
        }

        var existingEquipementInstalle = await _equipementInstalleManager.GetByIdAsync(id);

        if (existingEquipementInstalle == null || existingEquipementInstalle.Result is NotFoundResult)
        {
            return NotFound();
        }

        await _equipementInstalleManager.UpdateAsync(existingEquipementInstalle.Value, equipementInstalle);

        return NoContent();
    }


    // DELETE: api/EquipementInstalle/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEquipementInstalle(int id)
    {
        var equipementInstalle = await _equipementInstalleManager.GetByIdAsync(id);

        if (equipementInstalle == null || equipementInstalle.Result is NotFoundResult)
        {
            return NotFound();
        }

        await _equipementInstalleManager.DeleteAsync(equipementInstalle.Value);

        return NoContent();
    }
}
