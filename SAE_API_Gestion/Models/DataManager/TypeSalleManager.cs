using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Models.DataManager
{
    public class TypeSalleManager:IDataRepository<TypeSalle>
    {

        readonly GestionDBContext? gestionDbContext;
        public TypeSalleManager() { }
        public TypeSalleManager(GestionDBContext context)
        {
            gestionDbContext = context;
        }
        public async Task<ActionResult<IEnumerable<TypeSalle>>> GetAllAsync()
        {
            return await gestionDbContext.TypeSalles.ToListAsync();
        }
        public async Task<ActionResult<TypeSalle>> GetByIdAsync(int id)
        {
            return await gestionDbContext.TypeSalles.FirstOrDefaultAsync(u => u.TypeSalleId == id);
        }

        public async Task<ActionResult<TypeSalle>> GetByStringAsync(string str)
        {
            return await gestionDbContext.TypeSalles
                .FirstOrDefaultAsync(b => b.Nom == str);
        }

        public async Task AddAsync(TypeSalle entity)
        {
            await gestionDbContext.TypeSalles.AddAsync(entity);
            await gestionDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(TypeSalle typeCapteur, TypeSalle entity)
        {
            gestionDbContext.Entry(typeCapteur).State = EntityState.Modified;
            typeCapteur.TypeSalleId = entity.TypeSalleId;
            typeCapteur.Nom = entity.Nom;
            await gestionDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(TypeSalle typeCapteur)
        {
            gestionDbContext.TypeSalles.Remove(typeCapteur);
            await gestionDbContext.SaveChangesAsync();
        }
    }
}
