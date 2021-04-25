using System.ComponentModel.DataAnnotations;
using BLL.Base;

namespace BLL.App.DTO
{
    public class CarAccessType : BllEntityId
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarAccessType), Name = "Name")]
        public string? Name { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CarAccessType), Name = "AccessLevel")]
        public int? AccessLevel { get; set; }
    }
}