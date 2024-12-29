using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Models.DataManager
{
    public class TypeEquipementManager: IDataRepository<TypeEquipement>
    {
        readonly GestionDBContext? gestionDbContext;
        public TypeEquipementManager() { }
        public TypeEquipementManager(GestionDBContext context)
        {
            gestionDbContext = context;
        }
        public async Task<ActionResult<IEnumerable<TypeEquipement>>> GetAllAsync()
        {
            return await gestionDbContext.TypeEquipements.ToListAsync();
        }
        public async Task<ActionResult<TypeEquipement>> GetByIdAsync(int id)
        {
            return await gestionDbContext.TypeEquipements.FirstOrDefaultAsync(u => u.TypeEquipementId == id);
        }

        public async Task<ActionResult<TypeEquipement>> GetByStringAsync(string str)
        {
            return await gestionDbContext.TypeEquipements
                .FirstOrDefaultAsync(b => b.Nom == str);
        }

        public async Task AddAsync(TypeEquipement entity)
        {
            await gestionDbContext.TypeEquipements.AddAsync(entity);
            await gestionDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(TypeEquipement typeCapteur, TypeEquipement entity)
        {
            gestionDbContext.Entry(typeCapteur).State = EntityState.Modified;
            typeCapteur.TypeEquipementId = entity.TypeEquipementId;
            typeCapteur.Nom = entity.Nom;
            await gestionDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(TypeEquipement typeCapteur)
        {
            gestionDbContext.TypeEquipements.Remove(typeCapteur);
            await gestionDbContext.SaveChangesAsync();
        }
    }
}
