using System;
using System.Collections.Generic;
using DAL.Base;

namespace DAL.App.DTO
{
    public class CarModel : DalEntityId
    {
        public string Name { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        
        public Guid CarMarkId { get; set; }
        public CarMark? CarMark { get; set; }

        public ICollection<CarType> CarTypes { get; set; } = new List<CarType>();
    }
}