using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Models.DataManager
{
    public class MarqueCapteurManager : IDataRepository<MarqueCapteur>
    {
        readonly GestionDBContext? gestionDbContext;

        public MarqueCapteurManager() { }

        public MarqueCapteurManager(GestionDBContext context)
        {
            gestionDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<MarqueCapteur>>> GetAllAsync()
        {
            return await gestionDbContext.MarqueCapteurs
                .Include(m => m.Capteurs)
                .ToListAsync();
        }

        public async Task<ActionResult<MarqueCapteur>> GetByIdAsync(int id)
        {
            return await gestionDbContext.MarqueCapteurs
                .Include(m => m.Capteurs)
                .FirstOrDefaultAsync(m => m.MarqueId == id);
        }

        public async Task<ActionResult<MarqueCapteur>> GetByStringAsync(string str)
        {
            return await gestionDbContext.MarqueCapteurs
                .Include(m => m.Capteurs)
                .FirstOrDefaultAsync(m => m.Nom == str);
        }

        public async Task AddAsync(MarqueCapteur entity)
        {
            await gestionDbContext.MarqueCapteurs.AddAsync(entity);
            await gestionDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(MarqueCapteur marqueCapteur, MarqueCapteur entity)
        {
            gestionDbContext.Entry(marqueCapteur).State = EntityState.Modified;
            marqueCapteur.Nom = entity.Nom;
            marqueCapteur.Capteurs = entity.Capteurs;
            await gestionDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(MarqueCapteur marqueCapteur)
        {
            gestionDbContext.MarqueCapteurs.Remove(marqueCapteur);
            await gestionDbContext.SaveChangesAsync();
        }
    }
}
