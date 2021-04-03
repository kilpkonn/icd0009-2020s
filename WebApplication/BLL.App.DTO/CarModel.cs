using System;
using System.Collections.Generic;
using BLL.Base;

namespace BLL.App.DTO
{
    public class CarModel : BllEntity
    {
        public string? Name { get; set; }
        public DateTime? ReleaseDate { get; set; }
        
        public Guid? CarMarkId { get; set; }
        public CarMark? CarMark { get; set; }

        public ICollection<CarType>? CarTypes { get; set; } 
    }
}