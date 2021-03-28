using System;
using System.Collections.Generic;
using BLL.Base;
using Domain.App;
using Domain.Base;

namespace BLL.App.DTO
{
    public class CarModel : BllEntity
    {
        public string Name { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        
        public Guid CarMarkId { get; set; }
        public BLL.App.DTO.CarMark? CarMark { get; set; }

        public ICollection<CarType> CarTypes { get; set; } = new List<CarType>();
    }
}