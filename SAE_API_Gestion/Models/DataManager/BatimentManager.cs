using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Models.DataManager
{
    public class BatimentManager : IDataRepository<Batiment>
    {
        private readonly GestionDBContext? _gestionDbContext;

        public BatimentManager() { }

        public BatimentManager(GestionDBContext context)
        {
            _gestionDbContext = context;
        }


        public async Task<ActionResult<IEnumerable<Batiment>>> GetAllAsync()
        {
            return await _gestionDbContext.Batiments
                .Include(s => s.Salles)
                .ToListAsync();
        }



        public async Task<ActionResult<Batiment>> GetByIdAsync(int id)
        {
            return await _gestionDbContext.Batiments
                .Include(s => s.Salles)
                .FirstOrDefaultAsync(b => b.BatimentId == id);
        }



        public async Task<ActionResult<Batiment>> GetByStringAsync(string str)
        {
            var batiment = await _gestionDbContext.Batiments
                .Include(b => b.Salles)
                .FirstOrDefaultAsync(b => b.Nom == str);

            if (batiment == null)
            {
                return new NotFoundResult();
            }

            return batiment;
        }



        public async Task AddAsync(Batiment entity)
        {
            await _gestionDbContext.Batiments.AddAsync(entity);
            await _gestionDbContext.SaveChangesAsync();
        }




        public async Task UpdateAsync(Batiment batiment, Batiment entity)
        {
            if (batiment == null || entity == null) return;

            batiment.Nom = entity.Nom;
            batiment.ImageData = entity.ImageData;

            _gestionDbContext.Entry(batiment).State = EntityState.Modified;
            await _gestionDbContext.SaveChangesAsync();
        }




        public async Task DeleteAsync(Batiment batiment)
        {
            if (batiment == null) return;

            var salles = await _gestionDbContext.Salles
                .Where(s => s.BatimentId == batiment.BatimentId)
                .ToListAsync();

            if (salles.Any())
            {
                _gestionDbContext.Salles.RemoveRange(salles);
            }

            _gestionDbContext.Batiments.Remove(batiment);

            await _gestionDbContext.SaveChangesAsync();
        }



    }
}
