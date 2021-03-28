using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.Car
{
    public class CreateEditViewModel
    {
        public BLL.App.DTO.Car? Car { get; set; }
        public SelectList? CarTypeOptions { get; set; }
    }
}