using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.CarAccess
{
    public class CreateEditViewModel
    {
        public BLL.App.DTO.CarAccess? CarAccess { get; set; }
        public SelectList? CarOptions { get; set; } 
        public SelectList? CarAccessTypeOptions { get; set; }
    }
}