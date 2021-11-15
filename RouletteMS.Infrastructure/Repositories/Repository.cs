using Microsoft.EntityFrameworkCore;
using RouletteMS.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RouletteMS.Infrastructure.Repositories
{
    public class Repository<EntityType, IdType> : IRepository<EntityType, IdType> where EntityType : class, new()
    {
        private readonly RouletteContext _context;
        private readonly DbSet<EntityType> _entities;
        public Repository(RouletteContext context)
        {
            _context = context;
            _entities = _context.Set<EntityType>();
        }
        public void Add(EntityType entity)
        {
            _entities.Add(entity);
        }
        public void AddRange(IEnumerable<EntityType> entities)
        {
            _entities.AddRange(entities);
        }
        public void Delete(EntityType entity)
        {
            _entities.Remove(entity);
        }
        public void DeleteRange(IEnumerable<EntityType> entities)
        {
            _entities.RemoveRange(entities);
        }
        public async Task<EntityType> FindAsync(Expression<Func<EntityType, bool>> expression)
        {
            return await _entities.FirstOrDefaultAsync(expression);
        }
        public async Task<EntityType> GetAsync(IdType id)
        {
            return await _entities.FindAsync(id);
        }
        public async Task<IEnumerable<EntityType>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }
        public async Task<IEnumerable<EntityType>> GetWhereAsync(Expression<Func<EntityType, bool>> expression)
        {
            return await _entities.Where(expression).ToListAsync();
        }
        public void Update(EntityType entity)
        {
            _entities.Update(entity);
        }
        public void UpdateRange(IEnumerable<EntityType> entities)
        {
            _entities.UpdateRange(entities);
        }
    }
}
