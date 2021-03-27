using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Car.DAL.Base.Mappers;
using Car.DAL.Base.Repositories;
using Car.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class BaseRepository<TDalEntity, TDomainEntity, TDbContext> : BaseRepository<Guid, TDalEntity, TDomainEntity, TDbContext>
        where TDalEntity : class, IDomainEntityId<Guid>
        where TDomainEntity : class, IDomainEntityId<Guid>
        where TDbContext : DbContext
    {
        public BaseRepository(TDbContext dbContext, IBaseMapper<TDalEntity, TDomainEntity> mapper) : base(dbContext, mapper)
        {
        }
    }

    public class BaseRepository<TKey, TDalEntity, TDomainEntity, TDbContext> : IBaseRepository<TKey, TDalEntity>
        where TDalEntity : class, IDomainEntityId<TKey>
        where TDomainEntity : class, IDomainEntityId<TKey>
        where TKey : struct, IEquatable<TKey>
        where TDbContext : DbContext

    {
        protected readonly TDbContext DbContext;
        protected readonly DbSet<TDomainEntity> DbSet;
        protected readonly IBaseMapper<TDalEntity, TDomainEntity> Mapper;

        public BaseRepository(TDbContext dbContext, IBaseMapper<TDalEntity, TDomainEntity> mapper)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TDomainEntity>();
            Mapper = mapper;
        }

        public virtual async Task<IEnumerable<TDalEntity>> GetAllAsync(TKey? userId, bool tracking = false)
        {
            var query = CreateQuery(userId, tracking);
            return await query.Select(e => Mapper.Map(e)!).ToListAsync();
        }

        public virtual async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, TKey? userId, bool tracking = false)
        {
            var query = CreateQuery(userId, tracking);
            return await query.Select(e => Mapper.Map(e)!).FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public virtual TDalEntity Add(TDalEntity entity)
        {
            return Mapper.Map(DbSet.Add(Mapper.Map(entity)!).Entity)!;
        }

        public virtual TDalEntity Update(TDalEntity entity, TKey? userId)
        {
            if (userId != null && !userId.Equals(default) &&
                typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TDalEntity))
            )
            {
                throw new AuthenticationException("Bad user id inside entity to be deleted.");
                // TODO: load entity from the db, check that userId inside entity is correct.
            }
            
            return Mapper.Map(DbSet.Update(Mapper.Map(entity)!).Entity)!;
        }

        public virtual TDalEntity Remove(TDalEntity entity, TKey? userId)
        {
            if (userId != null && !((IDomainAppUserId<TKey>) entity).AppUserId.Equals(userId))
            {
                throw new AuthenticationException("Bad user id inside entity to be deleted.");
                // TODO: load entity from the db, check that userId inside entity is correct.
            }

            return Mapper.Map(DbSet.Remove(Mapper.Map(entity)!).Entity)!;
        }

        public virtual async Task<TDalEntity> RemoveAsync(TKey id, TKey? userId)
        {
            var entity = await FirstOrDefaultAsync(id, userId);
            if (entity == null) throw new NullReferenceException($"Entity with id {id} not found.");
            return Remove(entity!, userId);
        }

        public virtual async Task<bool> ExistsAsync(TKey id, TKey? userId)
        {
            return await DbSet.AnyAsync(e =>
                e.Id.Equals(id) && ((IDomainAppUserId<TKey>) e).AppUserId.Equals(userId));
        }

        protected IQueryable<TDomainEntity> CreateQuery(TKey? userId, bool tracking = false)
        {
            var query = DbSet.AsQueryable();
            // TODO: Also validate input entity
            if (userId != null && typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)))
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