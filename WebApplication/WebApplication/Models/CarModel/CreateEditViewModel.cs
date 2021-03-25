using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.CarModel
{
    public class CreateEditViewModel
    {
        public Domain.App.CarModel? CarModel { get; set; }
        
        public SelectList? CarMarks { get; set; } 
    }
}