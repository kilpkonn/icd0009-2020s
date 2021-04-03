using System;
using PublicApi.DTO.v1.Base;

namespace PublicApi.DTO.v1
{
    public class CarType : ApiDtoEntity
    {
        public string? Name { get; set; }

        public Guid? CarModelId { get; set; }
        public CarModel? CarModel { get; set; }
    }
}