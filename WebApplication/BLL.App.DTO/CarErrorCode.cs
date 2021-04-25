using System;
using System.ComponentModel.DataAnnotations;
using BLL.Base;
using Domain.Base;

namespace BLL.App.DTO
{
    public class CarErrorCode : BllEntity
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarErrorCode), Name = "CanId")]
        public int? CanId { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarErrorCode), Name = "Data")]
        public long? CanData { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarErrorCode), Name = "CarId")]
        public Guid? CarId { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarErrorCode), Name = "Car")]
        public Domain.App.Car? Car { get; set; }
    }
}