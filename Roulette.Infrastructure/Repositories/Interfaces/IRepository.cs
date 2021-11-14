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
        Task<EntityType> Get(IdType id);
        Task<EntityType> Find(Expression<Func<EntityType, bool>> expression);
        Task<IEnumerable<EntityType>> GetAll();
        Task<IEnumerable<EntityType>> GetWhere(Expression<Func<EntityType, bool>> expression);
        void Add(EntityType entity);
        void AddRange(IEnumerable<EntityType> entities);
        void Update(EntityType entity);
        void UpdateRange(IEnumerable<EntityType> entities);
        void Delete(EntityType entity);
        void DeleteRange(IEnumerable<EntityType> entities);
    }
}
