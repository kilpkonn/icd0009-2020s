using System;
using Domain.Base;

namespace BLL.App.DTO
{
    public class CarType : DomainEntity
    {
        public string Name { get; set; } = null!;
        
        public Guid CarModelId { get; set; }
        public BLL.App.DTO.CarModel? CarModel { get; set; }
    }
}