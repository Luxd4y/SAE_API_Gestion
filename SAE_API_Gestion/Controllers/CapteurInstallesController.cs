using Microsoft.AspNetCore.Mvc;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CapteurInstallesController : ControllerBase
{
    private readonly IDataRepository<CapteurInstalle> _capteurInstalleManager;

    public CapteurInstallesController(IDataRepository<CapteurInstalle> capteurInstalleManager)
    {
        _capteurInstalleManager = capteurInstalleManager;
    }


    // GET: api/CapteurInstalle
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CapteurInstalle>>> GetCapteursInstalles()
    {
        return await _capteurInstalleManager.GetAllAsync();
    }



    // GET: api/CapteurInstalle/5
    [HttpGet("[action]/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CapteurInstalle>> GetCapteurInstalleById(int id)
    {
        var capteurInstalle = await _capteurInstalleManager.GetByIdAsync(id);

        if (capteurInstalle.Result is NotFoundResult)
        {
            return NotFound();
        }

        return capteurInstalle.Value;
    }



    // POST: api/CapteurInstalle
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostCapteurInstalle(CapteurInstalle capteurInstalle)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _capteurInstalleManager.AddAsync(capteurInstalle);

        return CreatedAtAction(nameof(GetCapteurInstalleById), new { id = capteurInstalle.CapteurInstalleId }, capteurInstalle);
    }



    // PUT: api/CapteurInstalle/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutCapteurInstalle(int id, CapteurInstalle capteurInstalle)
    {
        if (id != capteurInstalle.CapteurInstalleId)
        {
            return BadRequest();
        }

        var existingCapteurInstalle = await _capteurInstalleManager.GetByIdAsync(id);

        if (existingCapteurInstalle.Result is NotFoundResult)
        {
            return NotFound();
        }

        await _capteurInstalleManager.UpdateAsync(existingCapteurInstalle.Value, capteurInstalle);

        return NoContent();
    }



    // DELETE: api/CapteurInstalle/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCapteurInstalle(int id)
    {
        var capteurInstalle = await _capteurInstalleManager.GetByIdAsync(id);

        if (capteurInstalle.Result is NotFoundResult)
        {
            return NotFound();
        }

        await _capteurInstalleManager.DeleteAsync(capteurInstalle.Value);

        return NoContent();
    }
}
