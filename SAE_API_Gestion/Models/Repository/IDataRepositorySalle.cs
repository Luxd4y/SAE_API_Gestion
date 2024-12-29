﻿using Microsoft.AspNetCore.Mvc;
using SAE_API_Gestion.Models.EntityFramework;

namespace SAE_API_Gestion.Models.Repository
{
    public interface IDataRepositorySalle
    {
        Task<ActionResult<IEnumerable<Salle>>> GetAllSalleByBatimentAsync(int batid);
    }
}