using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Car.DAL.Base.Repositories;
using Car.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class BaseRepository<TEntity, TDbContext> : BaseRepository<Guid, TEntity, TDbContext>
        where TEntity : class, IDomainEntityId<Guid>
        where TDbContext : DbContext
    {
        public BaseRepository(TDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class BaseRepository<TKey, TEntity, TDbContext> : IBaseRepository<TKey, TEntity>
        where TEntity : class, IDomainEntityId<TKey>
        where TKey : struct, IEquatable<TKey>
        where TDbContext : DbContext

    {
        protected readonly TDbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(TKey? userId, bool tracking = false)
        {
            var query = CreateQuery(userId, tracking);
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey? userId, bool tracking = false)
        {
            var query = CreateQuery(userId, tracking);
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public virtual TEntity Add(TEntity entity)
        {
            return DbSet.Add(entity).Entity;
        }

        public virtual TEntity Update(TEntity entity, TKey? userId)
        {
            if (userId != null && !((IDomainAppUserId<TKey>) entity).AppUserId.Equals(userId))
            {
                throw new AuthenticationException("Bad user id inside entity to be deleted.");
                // TODO: load entity from the db, check that userId inside entity is correct.
            }
            
            return DbSet.Update(entity).Entity;
        }

        public virtual TEntity Remove(TEntity entity, TKey? userId)
        {
            if (userId != null && !((IDomainAppUserId<TKey>) entity).AppUserId.Equals(userId))
            {
                throw new AuthenticationException("Bad user id inside entity to be deleted.");
                // TODO: load entity from the db, check that userId inside entity is correct.
            }

            return DbSet.Remove(entity).Entity;
        }

        public virtual async Task<TEntity> RemoveAsync(TKey id, TKey? userId)
        {
            var entity = await FirstOrDefaultAsync(id, userId);
            if (entity != null) throw new NullReferenceException($"Entity with id {id} not found.");
            return Remove(entity!, userId);
        }

        public virtual async Task<bool> ExistsAsync(TKey id, TKey? userId)
        {
            return await DbSet.AnyAsync(e =>
                e.Id.Equals(id) && ((IDomainAppUserId<TKey>) e).AppUserId.Equals(userId));
        }

        protected IQueryable<TEntity> CreateQuery(TKey? userId, bool tracking = false)
        {
            var query = DbSet.AsQueryable();

            if (userId != null && typeof(TEntity).IsAssignableFrom(typeof(IDomainAppUserId<TKey>)))
            {
                query = query.Where(e => ((IDomainAppUserId<TKey>) e).AppUserId.Equals(userId));
            }

            if (!tracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }
    }
}