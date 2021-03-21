using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.Car
{
    public class CreateEditViewModel
    {
        public Domain.App.Car? Car { get; set; }
        public SelectList? CarTypeOptions { get; set; }
    }
}