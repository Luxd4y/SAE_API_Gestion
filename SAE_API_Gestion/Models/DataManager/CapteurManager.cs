using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Models.DataManager
{
    public class CapteurManager : IDataRepository<Capteur>
    {
        private readonly GestionDBContext? _gestionDbContext;

        public CapteurManager() { }

        public CapteurManager(GestionDBContext context)
        {
            _gestionDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Capteur>>> GetAllAsync()
        {
            return await _gestionDbContext.Capteurs
                .Include(c => c.Marque) 
                .Include(c => c.CapteurInstalles) 
                .Include(c => c.ParametreCapteur)
                .ThenInclude(t => t.UniteMesurer)
                .ToListAsync();
        }




        public async Task<ActionResult<Capteur>> GetByIdAsync(int id)
        {
            var capteur = await _gestionDbContext.Capteurs
                .Include(c => c.Marque)
                .Include(c => c.CapteurInstalles)
                .Include(c => c.ParametreCapteur)
                .ThenInclude(t => t.UniteMesurer)
                .FirstOrDefaultAsync(c => c.CapteurId == id);

            if (capteur == null)
            {
                return new NotFoundResult();
            }

            return capteur;
        }



        public async Task<ActionResult<Capteur>> GetByStringAsync(string str)
        {
            var capteur = await _gestionDbContext.Capteurs
                .Include(c => c.Marque)
                .Include(c => c.CapteurInstalles)
                .Include(c => c.ParametreCapteur)
                .ThenInclude(t => t.UniteMesurer)
                .FirstOrDefaultAsync(c => c.Nom == str);

            if (capteur == null)
            {
                return new NotFoundResult();
            }

            return capteur;
        }


        public async Task AddAsync(Capteur entity)
        {
            await _gestionDbContext.Capteurs.AddAsync(entity);
            await _gestionDbContext.SaveChangesAsync();
        }




        public async Task UpdateAsync(Capteur capteur, Capteur entity)
        {
            if (capteur == null || entity == null) return;

            capteur.MarqueId = entity.MarqueId;
            capteur.Nom = entity.Nom;
            capteur.Description = entity.Description;
            capteur.Reference = entity.Reference;
            capteur.Hauteur = entity.Hauteur;
            capteur.Longueur = entity.Longueur;
            capteur.Largeur = entity.Largeur;
            capteur.Imagedata = entity.Imagedata;
            capteur.Marque = entity.Marque;
            capteur.ParametreCapteur = entity.ParametreCapteur;
            capteur.CapteurInstalles = entity.CapteurInstalles;

            _gestionDbContext.Entry(capteur).State = EntityState.Modified;
            await _gestionDbContext.SaveChangesAsync();
        }



        public async Task DeleteAsync(Capteur capteur)
        {
            if (capteur == null) return;

            _gestionDbContext.Capteurs.Remove(capteur);
            await _gestionDbContext.SaveChangesAsync();
        }
    }
}
