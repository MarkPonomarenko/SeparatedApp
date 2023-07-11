using Shared.Data;
using Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Shared.Repositories
{
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly AppDbContext _context;
        public DbSet<TEntity> Entities { get; set; }

        public EntityRepository(AppDbContext context) 
        {
            _context = context;
            Entities = _context.Set<TEntity>();
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await Entities.AddAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            TEntity? entity = await GetByIdAsync(id);
            if (entity != null)
            {
                Entities.Remove(entity);
                return true;
            }
            return false;
        }

        public IQueryable<TEntity> GetAll()
        {
            return Entities;
        }

        public Task<TEntity>? GetByIdAsync(Guid id)
        {
            return Entities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<TEntity> GetEntitiesByPage(int page, int pageSize)
        {
            if (page < 0 || pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(page));
            int ExcludeRecords = (pageSize * page) - pageSize;
            return Entities.OrderBy(x => x.Id).Skip(ExcludeRecords).Take(pageSize);
        }

        public IQueryable<TEntity> GetJoinEntities(params string[] columnsToJoin)
        {
            IQueryable<TEntity> query = Entities;
            foreach (var column in columnsToJoin)
            {
                query = query.Include(column);
            }
            return query;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> TotalCountOfEntitiesAsync()
        {
            return Entities.CountAsync();
        }

        public bool Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            Entities.Update(entity);
            return true;
        }
    }
}
