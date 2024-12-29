using Microsoft.AspNetCore.Mvc;
using SAE_API_Gestion.Models.EntityFramework;

namespace SAE_API_Gestion.Models.Repository
{
    public interface IDataRepositoryParametreCapteur
    {
        Task<ActionResult<ParametreCapteur>> GetJoinByDoubleIdAsync(int id1, int id2);
    }
}
