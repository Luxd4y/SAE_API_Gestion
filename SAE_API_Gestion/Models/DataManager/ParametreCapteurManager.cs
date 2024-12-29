using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API_Gestion.Models.EntityFramework;
using SAE_API_Gestion.Models.Repository;

namespace SAE_API_Gestion.Models.DataManager
{
    public class ParametreCapteurManager: IDataRepository<ParametreCapteur>,IDataRepositoryParametreCapteur
    {
    
        readonly GestionDBContext? context;

        public ParametreCapteurManager() { }

        public ParametreCapteurManager(GestionDBContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult<IEnumerable<ParametreCapteur>>> GetAllAsync()
        {
            return await context.ParametreCapteur
                .ToListAsync();
        }

        public async Task AddAsync(ParametreCapteur entity)
        {
            context.ParametreCapteur.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ParametreCapteur entity)
        {
            context.ParametreCapteur.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ParametreCapteur album, ParametreCapteur entity)
        {
            context.Entry(album).State = EntityState.Modified;

            album.UniteMesurerId = entity.UniteMesurerId;
            album.CapteurId = entity.CapteurId;
            album.AcuPlagemin = entity.AcuPlagemin;
            album.AcuPlagemax = entity.AcuPlagemax;
            album.AcuPrecision = entity.AcuPrecision;
            await context.SaveChangesAsync();

        }

      
        public async Task<ActionResult<ParametreCapteur>> GetJoinByDoubleIdAsync(int id1, int id2)
        {
            return await context.ParametreCapteur
                 .FirstOrDefaultAsync(item => item.CapteurId == id1 && item.UniteMesurerId == id2);
        }

        public Task<ActionResult<ParametreCapteur>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ParametreCapteur>> GetByStringAsync(string str)
        {
            throw new NotImplementedException();
        }
    }
}
