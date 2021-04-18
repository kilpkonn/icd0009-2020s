using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Car.DAL.Base.Mappers;
using Car.DAL.Base.Models;
using Car.DAL.Base.Repositories;
using Car.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    /// <inheritdoc />
    public class
        BaseRepository<TDalEntity, TDomainEntity, TDbContext> : BaseRepository<Guid, TDalEntity, TDomainEntity,
            TDbContext>
        where TDalEntity : class, IDalEntityId<Guid>
        where TDomainEntity : class, IDomainEntityId<Guid>
        where TDbContext : DbContext
    {
        /// <inheritdoc />
        public BaseRepository(TDbContext dbContext, IBaseMapper<TDalEntity, TDomainEntity> mapper) : base(dbContext,
            mapper)
        {
        }
    }

    /// <inheritdoc />
    public class BaseRepository<TKey, TDalEntity, TDomainEntity, TDbContext> : IBaseRepository<TKey, TDalEntity>
        where TDalEntity : class, IDalEntityId<TKey>
        where TDomainEntity : class, IDomainEntityId<TKey>
        where TKey : struct, IEquatable<TKey>
        where TDbContext : DbContext

    {
        /// <summary>
        /// Database context
        /// </summary>
        protected readonly TDbContext DbContext;
        /// <summary>
        /// Database set (table)
        /// </summary>
        protected readonly DbSet<TDomainEntity> DbSet;
        /// <summary>
        /// Entity mapper
        /// </summary>
        protected readonly IBaseMapper<TDalEntity, TDomainEntity> Mapper;
        private readonly Dictionary<TDalEntity, TDomainEntity> _entityCache = new();

        /// <summary>
        /// Base repository for CRUD
        /// </summary>
        /// <param name="dbContext">Database context</param>
        /// <param name="mapper">Entity mapper</param>
        public BaseRepository(TDbContext dbContext, IBaseMapper<TDalEntity, TDomainEntity> mapper)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TDomainEntity>();
            Mapper = mapper;
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TDalEntity>> GetAllAsync(TKey? userId, bool tracking = false)
        {
            var query = CreateQuery(userId, tracking);
            return await query.Select(e => Mapper.Map(e)!).ToListAsync();
        }

        /// <inheritdoc />
        public virtual async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, TKey? userId, bool tracking = false)
        {
            var query = CreateQuery(userId, tracking);
            return Mapper.Map(await query.FirstOrDefaultAsync(e => e.Id.Equals(id)));
        }

        /// <inheritdoc />
        public virtual TDalEntity Add(TDalEntity entity)
        {
            var domainEntity = Mapper.Map(entity)!;
            var updatedDomainEntity = DbSet.Add(domainEntity).Entity;
            var dalEntity = Mapper.Map(updatedDomainEntity)!;

            _entityCache.Add(entity, domainEntity);

            return dalEntity;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public virtual TDalEntity Remove(TDalEntity entity, TKey? userId)
        {
            var dbEntity = Mapper.Map(entity)!;
            if (userId != null && !((IDomainAppUserId<TKey>) dbEntity).AppUserId.Equals(userId))
            {
                throw new AuthenticationException("Bad user id inside entity to be deleted.");
                // TODO: load entity from the db, check that userId inside entity is correct.
            }

            return Mapper.Map(DbSet.Remove(dbEntity).Entity)!;
        }

        /// <inheritdoc />
        public virtual async Task<TDalEntity> RemoveAsync(TKey id, TKey? userId)
        {
            var entity = await FirstOrDefaultAsync(id, userId);
            if (entity == null) throw new NullReferenceException($"Entity with id {id} not found.");
            return Remove(entity!, userId);
        }

        /// <inheritdoc />
        public virtual async Task<bool> ExistsAsync(TKey id, TKey? userId)
        {
            return await DbSet.AnyAsync(e =>
                e.Id.Equals(id) && ((IDomainAppUserId<TKey>) e).AppUserId.Equals(userId));
        }

        /// <inheritdoc />
        public TDalEntity GetUpdatedEntityAfterSaveChanges(TDalEntity entity)
        {
            var updatedEntity = _entityCache[entity]!;
            var dalEntity = Mapper.Map(updatedEntity)!;

            return dalEntity;
        }


        /// <summary>
        /// Query builder helper
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="tracking">Boolean to optionally add tracking</param>
        /// <returns>Query with user id check and tracking set</returns>
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