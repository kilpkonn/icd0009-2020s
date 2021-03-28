using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.CarModel
{
    public class CreateEditViewModel
    {
        public BLL.App.DTO.CarModel? CarModel { get; set; }
        
        public SelectList? CarMarks { get; set; } 
    }
}