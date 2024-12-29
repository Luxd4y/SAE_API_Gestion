using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Models.DataManager
{
    public class EquipementInstalleManager : IDataRepository<EquipementInstalle>
    {
        readonly GestionDBContext? gestionDbContext;

        public EquipementInstalleManager() { }

        public EquipementInstalleManager(GestionDBContext context)
        {
            gestionDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<EquipementInstalle>>> GetAllAsync()
        {
            return await gestionDbContext.EquipementInstalles
                .Include(e => e.Equipement)
                .Include(e => e.Salle)
                .Include(e => e.Surface)
                .ToListAsync();
        }

        public async Task<ActionResult<EquipementInstalle>> GetByIdAsync(int id)
        {
            return await gestionDbContext.EquipementInstalles
                .Include(e => e.Equipement)
                .Include(e => e.Salle)
                .Include(e => e.Surface)
                .FirstOrDefaultAsync(e => e.EquipementInstalleId == id);
        }

        public async Task<ActionResult<EquipementInstalle>> GetByStringAsync(string str)
        {
            throw new NotImplementedException("Recherche par chaîne non applicable pour EquipementInstalle.");
        }

        public async Task AddAsync(EquipementInstalle entity)
        {
            await gestionDbContext.EquipementInstalles.AddAsync(entity);
            await gestionDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(EquipementInstalle equipementInstalle, EquipementInstalle entity)
        {
            gestionDbContext.Entry(equipementInstalle).State = EntityState.Modified;
            equipementInstalle.SalleId = entity.SalleId;
            equipementInstalle.EquipementId = entity.EquipementId;
            equipementInstalle.SurfaceId = entity.SurfaceId;
            equipementInstalle.PositionX = entity.PositionX;
            equipementInstalle.PositionY = entity.PositionY;
            await gestionDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(EquipementInstalle equipementInstalle)
        {
            gestionDbContext.EquipementInstalles.Remove(equipementInstalle);
            await gestionDbContext.SaveChangesAsync();
        }
    }
}
