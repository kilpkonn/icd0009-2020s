using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.Track
{
    public class CreateEditViewModel
    {
        public Domain.App.Track? Track { get; set; }
        
        public SelectList? Cars { get; set; } 
    }
}