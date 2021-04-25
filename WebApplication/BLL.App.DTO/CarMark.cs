using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.Base;

namespace BLL.App.DTO
{
    public class CarMark : BllEntity
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarMark), Name = "Name")]
        public string? Name { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarMark), Name = "Models")]
        public ICollection<CarModel>? CarModels { get; set; }
    }
}