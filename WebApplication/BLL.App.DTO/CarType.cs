using System;
using BLL.Base;

namespace BLL.App.DTO
{
    public class CarType : BllEntity
    {
        public string? Name { get; set; }

        public Guid? CarModelId { get; set; }
        public CarModel? CarModel { get; set; }
    }
}