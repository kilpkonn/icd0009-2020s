using System.Collections.Generic;
using BLL.Base;

namespace BLL.App.DTO
{
    public class CarMark : BllEntity
    {
        public string? Name { get; set; } 

        public ICollection<CarModel>? CarModels { get; set; } 
    }
}