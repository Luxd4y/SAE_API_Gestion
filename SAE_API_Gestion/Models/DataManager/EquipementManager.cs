using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Models.DataManager
{
    public class EquipementManager : IDataRepository<Equipement>
    {
        private readonly GestionDBContext? _gestionDbContext;

        public EquipementManager() { }


        public EquipementManager(GestionDBContext context)
        {
            _gestionDbContext = context;
        }


        public async Task<ActionResult<IEnumerable<Equipement>>> GetAllAsync()
        {
            return await _gestionDbContext.Equipements
                .Include(e => e.TypeEquipement)
                .Include(e => e.EquipementInstalles)
                .ToListAsync();
        }



        public async Task<ActionResult<Equipement>> GetByIdAsync(int id)
        {
            var equipement = await _gestionDbContext.Equipements
                .Include(e => e.TypeEquipement)
                .Include(e => e.EquipementInstalles)
                .FirstOrDefaultAsync(e => e.EquipementId == id);

            if (equipement == null)
            {
                return new NotFoundResult();
            }

            return equipement;
        }



        public async Task<ActionResult<Equipement>> GetByStringAsync(string str)
        {
            var equipement = await _gestionDbContext.Equipements
                .Include(e => e.TypeEquipement)
                .Include(e => e.EquipementInstalles)
                .FirstOrDefaultAsync(e => e.Nom == str);

            if (equipement == null)
            {
                return new NotFoundResult();
            }

            return equipement;
        }




        public async Task AddAsync(Equipement entity)
        {
            await _gestionDbContext.Equipements.AddAsync(entity);
            await _gestionDbContext.SaveChangesAsync();
        }




        public async Task UpdateAsync(Equipement equipement, Equipement entity)
        {
            if (equipement == null || entity == null) return;

            equipement.TypeEquipementId = entity.TypeEquipementId;
            equipement.Nom = entity.Nom;
            equipement.Hauteur = entity.Hauteur;
            equipement.Longueur = entity.Longueur;
            equipement.Largeur = entity.Largeur;
            equipement.ImageData = entity.ImageData;

            _gestionDbContext.Entry(equipement).State = EntityState.Modified;
            await _gestionDbContext.SaveChangesAsync();
        }




        public async Task DeleteAsync(Equipement equipement)
        {
            if (equipement == null) return;

            _gestionDbContext.Equipements.Remove(equipement);
            await _gestionDbContext.SaveChangesAsync();
        }
    }
}
