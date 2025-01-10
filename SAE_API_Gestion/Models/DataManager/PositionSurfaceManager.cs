using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Models.DataManager
{
    public class PositionSurfaceManager : IDataRepository<PositionSurface>
    {
        readonly GestionDBContext? gestionDbContext;

        public PositionSurfaceManager() { }

        public PositionSurfaceManager(GestionDBContext context)
        {
            gestionDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<PositionSurface>>> GetAllAsync()
        {
            return await gestionDbContext.PositionSurfaces
                .ToListAsync();
        }

        public async Task<ActionResult<PositionSurface>> GetByIdAsync(int id)
        {
            return await gestionDbContext.PositionSurfaces
                .FirstOrDefaultAsync(p => p.PositionSurfaceId == id);
        }

        public async Task<ActionResult<PositionSurface>> GetByStringAsync(string str)
        {
            return await gestionDbContext.PositionSurfaces
                .FirstOrDefaultAsync(p => p.Nom == str);
        }

        public async Task AddAsync(PositionSurface entity)
        {
            await gestionDbContext.PositionSurfaces.AddAsync(entity);
            await gestionDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PositionSurface positionSurface, PositionSurface entity)
        {
            gestionDbContext.Entry(positionSurface).State = EntityState.Modified;
            positionSurface.Nom = entity.Nom;
            await gestionDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(PositionSurface positionSurface)
        {
            gestionDbContext.PositionSurfaces.Remove(positionSurface);
            await gestionDbContext.SaveChangesAsync();
        }
    }
}
