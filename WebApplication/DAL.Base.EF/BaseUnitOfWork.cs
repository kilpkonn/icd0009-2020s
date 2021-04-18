using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    /// <inheritdoc />
    public class BaseUnitOfWork<TDbContext> : BaseUnitOfWork
        where TDbContext: DbContext
    {
        /// <summary>
        /// Database context
        /// </summary>
        protected readonly TDbContext UowDbContext;

        /// <inheritdoc />
        public BaseUnitOfWork(TDbContext uowDbContext)
        {
            UowDbContext = uowDbContext;
        }

        /// <inheritdoc />
        public override Task<int> SaveChangesAsync()
        {
            return UowDbContext.SaveChangesAsync();
        }
        
        private readonly Dictionary<Type, object> _repoCache = new();

        /// <summary>
        /// Helper function for getting repositories and cacheing them
        /// </summary>
        /// <param name="repoCreationMethod">Method to create repo if none exsists</param>
        /// <typeparam name="TRepository">Repo type</typeparam>
        /// <returns></returns>
        protected TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod) where TRepository : class
        {
            if (_repoCache.TryGetValue(typeof(TRepository), out var repo))
            {
                return (TRepository) repo;
            }

            var newRepo = repoCreationMethod();
            _repoCache.Add(typeof(TRepository), newRepo);
            return newRepo;
        }
    }
}