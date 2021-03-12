using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    public class BaseUnitOfWork<TDbContext> : DAL.Base.BaseUnitOfWork
        where TDbContext: DbContext
    {
        protected readonly TDbContext UowDbContext;
        
        public BaseUnitOfWork(TDbContext uowDbContext)
        {
            UowDbContext = uowDbContext;
        }
        
        public override Task<int> SaveChangesAsync()
        {
            return UowDbContext.SaveChangesAsync();
        }
        
        private readonly Dictionary<Type, object> _repoCache = new();

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