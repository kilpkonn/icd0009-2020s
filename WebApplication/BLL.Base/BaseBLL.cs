using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Car.DAL.Base;
using CarApp.BLL.Base;

namespace BLL.Base
{
    public class BaseBll<TUnitOfWork> : IBaseBll
        where TUnitOfWork: IBaseUnitOfWork
    {
        protected readonly TUnitOfWork Uow;

        public BaseBll(TUnitOfWork uow)
        {
            this.Uow = uow;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Uow.SaveChangesAsync();
        }

        private readonly Dictionary<Type, object> _serviceCache = new();

        public TService GetService<TService>(Func<TService> serviceCreationMethod) where TService : class
        {
            if (_serviceCache.TryGetValue(typeof(TService), out var repo))
            {
                return (TService) repo;
            }

            var repoInstance = serviceCreationMethod();
            _serviceCache.Add(typeof(TService), repoInstance);
            return repoInstance;
        }
    }

}