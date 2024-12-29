using Microsoft.AspNetCore.Mvc;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BatimentsController : ControllerBase
{
    private readonly IDataRepository<Batiment> _batimentManager;

    public BatimentsController(IDataRepository<Batiment> batimentManager)
    {
        _batimentManager = batimentManager;
    }


    // GET: api/Batiments
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Batiment>>> GetBatiments()
    {
        return await _batimentManager.GetAllAsync();
    }


    // GET: api/Batiments/5
    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetBatimentById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Batiment>> GetBatimentById(int id)
    {
        var batiment = await _batimentManager.GetByIdAsync(id);

        if (batiment == null)
        {
            return NotFound();
        }

        return batiment;
    }



    // GET: api/Batiments/ByName/{name}
    [HttpGet("{str}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Batiment>> GetBatimentByNom(string str)
    {
        var batiment = await _batimentManager.GetByStringAsync(str);

        if (batiment.Result is NotFoundResult)
        {
            return NotFound();
        }

        return batiment.Value;
    }



    // POST: api/Batiments
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostBatiment(Batiment batiment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _batimentManager.AddAsync(batiment);

        return CreatedAtAction("GetById", new { id = batiment.BatimentId }, batiment);
    }


    [HttpPost]
    [Route("[action]/{id}")]
    [ActionName("UploadImage")]
    public async Task<ActionResult<Batiment>> UploadImage(int id, IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var imageData = memoryStream.ToArray();

                var batiment = await _batimentManager.GetByIdAsync(id);
                if (batiment == null)
                {
                    return NotFound();
                }

                batiment.Value.ImageData = imageData;
                await _batimentManager.UpdateAsync(batiment.Value, batiment.Value);
                return NoContent();
            }
        }

        return BadRequest();
    }


    // PUT: api/Batiments/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutBatiment(int id, Batiment batiment)
    {
        if (id != batiment.BatimentId)
        {
            return BadRequest();
        }

        var existingBatiment = await _batimentManager.GetByIdAsync(id);

        if (existingBatiment.Result is NotFoundResult)
        {
            return NotFound();
        }

        await _batimentManager.UpdateAsync(existingBatiment.Value, batiment);

        return NoContent();
    }


    // DELETE: api/Batiments/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBatiment(int id)
    {
        var batiment = await _batimentManager.GetByIdAsync(id);

        if (batiment.Result is NotFoundResult)
        {
            return NotFound();
        }

        await _batimentManager.DeleteAsync(batiment.Value);

        return NoContent();
    }
}
