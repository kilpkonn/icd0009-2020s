using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.CarType
{
    public class CreateEditViewModel
    {
        public BLL.App.DTO.CarType? CarType { get; set; }
        
        public SelectList? CarModels { get; set; } 
    }
}