using System;
using System.ComponentModel.DataAnnotations;
using BLL.Base;

namespace BLL.App.DTO
{
    public class CarType : BllEntity
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarType), Name = "Name")]
        public string? Name { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarType), Name = "ModelId")]
        public Guid? CarModelId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarType), Name = "Model")]
        public CarModel? CarModel { get; set; }
    }
}