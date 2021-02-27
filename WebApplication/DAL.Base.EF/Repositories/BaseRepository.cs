using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Car.DAL.Base.Repositories;
using Car.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class BaseRepository<TEntity> : BaseRepository<Guid, TEntity>
        where TEntity : class, IDomainEntityId<Guid>
    {
        public BaseRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public class BaseRepository<TKey, TEntity> : IBaseRepository<TKey, TEntity>
        where TEntity : class, IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>

    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false)
        {
            var query = DbSet.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(TKey id, bool tracking = false)
        {
            var query = DbSet.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public TEntity Add(TEntity entity)
        {
            return DbSet.Add(entity).Entity;
        }

        public TEntity Update(TEntity entity)
        {
            return DbSet.Update(entity).Entity;
        }

        public TEntity Remove(TEntity entity)
        {
            return DbSet.Remove(entity).Entity;
        }

        public async Task<TEntity> Remove(TKey id)
        {
            var entity = await FirstOrDefaultAsync(id);
            return DbSet.Remove(entity).Entity;
        }

        public async Task<bool> ExistsAsync(TKey id)
        {
            return await DbSet.AnyAsync(e => e.Id.Equals(id));
        }
    }
}