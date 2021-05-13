using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.Base;

namespace BLL.App.DTO
{
    public class CarModel : BllEntity
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarModel), Name = "Name")]
        public string? Name { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarModel), Name = "ReleaseDate")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarModel), Name = "CarMarkId")]
        public Guid? CarMarkId { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarModel), Name = "CarMark")]
        public CarMark? CarMark { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarModel), Name = "Types")]

        public ICollection<CarType>? CarTypes { get; set; }
    }
}