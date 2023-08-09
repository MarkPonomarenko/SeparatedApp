namespace Shared.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        Task<int> TotalCountOfEntitiesAsync();
        IQueryable<TEntity> GetAll();
        Task<bool> AddAsync(TEntity entity);
        bool Update(TEntity entity);
        Task<bool> DeleteAsync(Guid id);
        Task<TEntity>? GetByIdAsync(Guid id);
        Task<int> SaveChangesAsync();
        IQueryable<TEntity> GetJoinEntities(params string[] columnsToJoin);
        IQueryable<TEntity> GetEntitiesByPage(int page, int pageSize);

    }
}
