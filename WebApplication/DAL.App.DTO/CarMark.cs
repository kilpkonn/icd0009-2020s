﻿using System.Collections.Generic;
using DAL.Base;

namespace DAL.App.DTO
{
    public class CarMark : DalEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();
    }
}