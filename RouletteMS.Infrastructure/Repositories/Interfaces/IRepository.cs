using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RouletteMS.Infrastructure.Repositories
{
    public interface IRepository<EntityType, IdType> where EntityType : class, new()
    {
        Task<EntityType> GetAsync(IdType id);
        Task<EntityType> FindAsync(Expression<Func<EntityType, bool>> expression);
        Task<IEnumerable<EntityType>> GetAllAsync();
        Task<IEnumerable<EntityType>> GetWhereAsync(Expression<Func<EntityType, bool>> expression);
        void Add(EntityType entity);
        void AddRange(IEnumerable<EntityType> entities);
        void Update(EntityType entity);
        void UpdateRange(IEnumerable<EntityType> entities);
        void Delete(EntityType entity);
        void DeleteRange(IEnumerable<EntityType> entities);
    }
}
