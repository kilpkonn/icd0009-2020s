using System;
using Car.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class CarType : DomainEntity
    {
        public Guid NameId { get; set; }
        public LangString? Name { get; set; }
        
        public Guid CarModelId { get; set; }
        public CarModel? CarModel { get; set; }
    }
}