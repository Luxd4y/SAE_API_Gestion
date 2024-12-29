using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Models.DataManager
{
    public class CapteurInstalleManager : IDataRepository<CapteurInstalle>
    {
        private readonly GestionDBContext? _gestionDbContext;

        public CapteurInstalleManager() { }

        public CapteurInstalleManager(GestionDBContext context)
        {
            _gestionDbContext = context;
        }
          

        public async Task<ActionResult<IEnumerable<CapteurInstalle>>> GetAllAsync()
        {
            return await _gestionDbContext.CapteurInstalles
                .Include(ci => ci.Capteur) 
                .Include(ci => ci.Salle)  
                .Include(ci => ci.Surface) 
                .ToListAsync();
        }

        public async Task<ActionResult<CapteurInstalle>> GetByIdAsync(int id)
        {
            var capteurInstalle = await _gestionDbContext.CapteurInstalles
                .Include(ci => ci.Capteur)
                .Include(ci => ci.Salle)
                .Include(ci => ci.Surface)
                .FirstOrDefaultAsync(ci => ci.CapteurInstalleId == id);

            if (capteurInstalle == null)
            {
                return new NotFoundResult();
            }

            return capteurInstalle;
        }


        public async Task AddAsync(CapteurInstalle entity)
        {
            await _gestionDbContext.CapteurInstalles.AddAsync(entity);
            await _gestionDbContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(CapteurInstalle capteurInstalle, CapteurInstalle entity)
        {
            if (capteurInstalle == null || entity == null) return;

            capteurInstalle.SurfaceId = entity.SurfaceId;
            capteurInstalle.CapteurId = entity.CapteurId;
            capteurInstalle.SalleId = entity.SalleId;
            capteurInstalle.PositionX = entity.PositionX;
            capteurInstalle.PositionY = entity.PositionY;
            capteurInstalle.Capteur = entity.Capteur;
            capteurInstalle.Salle = entity.Salle;
            capteurInstalle.Surface = entity.Surface;

            _gestionDbContext.Entry(capteurInstalle).State = EntityState.Modified;
            await _gestionDbContext.SaveChangesAsync();
        }


        public async Task DeleteAsync(CapteurInstalle capteurInstalle)
        {
            if (capteurInstalle == null) return;

            _gestionDbContext.CapteurInstalles.Remove(capteurInstalle);
            await _gestionDbContext.SaveChangesAsync();
        }


        public Task<ActionResult<CapteurInstalle>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }
    }
}
