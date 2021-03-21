using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.CarErrorCode
{
    public class CreateEditViewModel
    {
        public Domain.App.CarErrorCode? CarErrorCode { get; set; }
        public SelectList? CarOptions { get; set; } 
    }
}