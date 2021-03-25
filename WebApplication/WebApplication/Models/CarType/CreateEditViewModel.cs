using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.CarType
{
    public class CreateEditViewModel
    {
        public Domain.App.CarType? CarType { get; set; }
        
        public SelectList? CarModels { get; set; } 
    }
}