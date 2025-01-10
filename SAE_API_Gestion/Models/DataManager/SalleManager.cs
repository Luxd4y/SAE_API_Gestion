using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Models.DataManager
{
    public class SalleManager : IDataRepository<Salle>,IDataRepositorySalle
    {
        readonly GestionDBContext? gestionDbContext;

        public SalleManager() { }

        public SalleManager(GestionDBContext context)
        {
            gestionDbContext = context;
        }



        public async Task<ActionResult<IEnumerable<Salle>>> GetAllAsync()
        {
            return await gestionDbContext.Salles
                .Include(s => s.TypeSalle)
                .ToListAsync();
        }

        public async Task<ActionResult<Salle>> GetByIdAsync(int id)
        {
            return await gestionDbContext.Salles
                .Include(s => s.TypeSalle) 
                .FirstOrDefaultAsync(s => s.SalleId == id);
        }


        public async Task<ActionResult<IEnumerable<Salle>>> GetAllSalleByBatimentAsync(int batid)
        {
            return await gestionDbContext.Salles
                .Include(s => s.Batiment)
                .Include(s => s.TypeSalle)
                .Where(e => e.BatimentId == batid).ToListAsync();
        }



        public async Task<ActionResult<Salle>> GetByStringAsync(string str)
        {
            return await gestionDbContext.Salles
                .Include(s => s.TypeSalle)
                .FirstOrDefaultAsync(s => s.Nom == str);
        }

        public async Task AddAsync(Salle entity)
        {
            await gestionDbContext.Salles.AddAsync(entity);
            await gestionDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Salle salle, Salle entity)
        {
            gestionDbContext.Entry(salle).State = EntityState.Modified;
            salle.SalleId = entity.SalleId;
            salle.BatimentId = entity.BatimentId;
            salle.TypeSalleId = entity.TypeSalleId;
            salle.Nom = entity.Nom;
            salle.Capacite = entity.Capacite;
            salle.ImageData = entity.ImageData;
            salle.CapteurInstalles = entity.CapteurInstalles;
            salle.EquipementInstalles = entity.EquipementInstalles;
            salle.Surfaces = entity.Surfaces;
            await gestionDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Salle salle)
        {
            gestionDbContext.Salles.Remove(salle);
            await gestionDbContext.SaveChangesAsync();
        }

     
    }
}
