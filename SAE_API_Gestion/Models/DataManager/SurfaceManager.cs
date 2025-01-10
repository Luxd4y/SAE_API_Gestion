using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Models.DataManager
{
    public class SurfaceManager : IDataRepository<Surface>, IDataRepositorySurface
    {
        readonly GestionDBContext? gestionDbContext;
        public SurfaceManager() { }
        public SurfaceManager(GestionDBContext context)
        {
            gestionDbContext = context;
        }
        public async Task<ActionResult<IEnumerable<Surface>>> GetAllAsync()
        {
            return await gestionDbContext.Surfaces.ToListAsync();
        }
        public async Task<ActionResult<Surface>> GetByIdAsync(int id)
        {
            return await gestionDbContext.Surfaces.FirstOrDefaultAsync(u => u.SurfaceId == id);
        }


        public async Task AddAsync(Surface entity)
        {
            await gestionDbContext.Surfaces.AddAsync(entity);
            await gestionDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Surface typeCapteur, Surface entity)
        {
            gestionDbContext.Entry(typeCapteur).State = EntityState.Modified;
            typeCapteur.SurfaceId = entity.SurfaceId;
            typeCapteur.SalleId = entity.SalleId;
            typeCapteur.PositionSurfaceId = entity.PositionSurfaceId;
            typeCapteur.Longueur = entity.Longueur;
            typeCapteur.Hauteur = entity.Hauteur;
            await gestionDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Surface typeCapteur)
        {
            gestionDbContext.Surfaces.Remove(typeCapteur);
            await gestionDbContext.SaveChangesAsync();
        }


        public Task<ActionResult<Surface>> GetByStringAsync(string str)
        {
            throw new NotImplementedException("Pas besoin de GetByString");
        }

        public async Task<ActionResult<IEnumerable<Surface>>> GetSurfacesBySalleIdAsync(int salleId)
        {
            return await gestionDbContext.Surfaces
                .Include(s => s.PositionSurface)  
                .Where(s => s.SalleId == salleId)
                .ToListAsync();
        }

    }
}
