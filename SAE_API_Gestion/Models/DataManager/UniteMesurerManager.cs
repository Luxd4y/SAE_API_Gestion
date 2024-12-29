using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Models.DataManager
{
    public class UniteMesurerManager : IDataRepository<UniteMesurer>
    {
        readonly GestionDBContext? gestionDbContext;
        public UniteMesurerManager() { }
        public UniteMesurerManager(GestionDBContext context)
        {
            gestionDbContext = context;
        }
        public async Task<ActionResult<IEnumerable<UniteMesurer>>> GetAllAsync()
        {
            return await gestionDbContext.UniteMesurers.ToListAsync();
        }
        public async Task<ActionResult<UniteMesurer>> GetByIdAsync(int id)
        {
            return await gestionDbContext.UniteMesurers.FirstOrDefaultAsync(u => u.UniteMesurerId == id);
        }

        public async Task<ActionResult<UniteMesurer>> GetByStringAsync(string str)
        {
            return await gestionDbContext.UniteMesurers
                .FirstOrDefaultAsync(b => b.Nom == str);
        }

        public async Task AddAsync(UniteMesurer entity)
        {
            await gestionDbContext.UniteMesurers.AddAsync(entity);
            await gestionDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(UniteMesurer typeCapteur, UniteMesurer entity)
        {
            gestionDbContext.Entry(typeCapteur).State = EntityState.Modified;
            typeCapteur.UniteMesurerId = entity.UniteMesurerId;
            typeCapteur.Nom = entity.Nom;
            await gestionDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(UniteMesurer typeCapteur)
        {
            gestionDbContext.UniteMesurers.Remove(typeCapteur);
            await gestionDbContext.SaveChangesAsync();
        }
    }
}
