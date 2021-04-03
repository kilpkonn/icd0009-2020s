using System;
using System.Collections.Generic;
using BLL.App.DTO;
using PublicApi.DTO.v1.Base;

namespace PublicApi.DTO.v1
{
    public class CarModel : ApiDtoEntity
    {
        public string? Name { get; set; } = null!;
        public DateTime? ReleaseDate { get; set; }
        
        public Guid? CarMarkId { get; set; }
        public CarMark? CarMark { get; set; }

        public ICollection<CarType>? CarTypes { get; set; }
    }
}