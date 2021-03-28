using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.CarErrorCode
{
    public class CreateEditViewModel
    {
        public BLL.App.DTO.CarErrorCode? CarErrorCode { get; set; }
        public SelectList? CarOptions { get; set; } 
    }
}