using Microsoft.AspNetCore.Mvc;
using SAE_API_Gestion.Models.EntityFramework;

namespace SAE_API_Gestion.Models.Repository
{
    public interface IDataRepositorySurface
    {
        Task<ActionResult<IEnumerable<Surface>>> GetSurfacesBySalleIdAsync(int salleId);

    }
}
