using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.Track
{
    public class CreateEditViewModel
    {
        public BLL.App.DTO.Track? Track { get; set; }
        
        public SelectList? Cars { get; set; } 
    }
}