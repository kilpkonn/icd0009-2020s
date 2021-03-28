using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.TrackLocation
{
    public class CreateEditViewModel
    {
        public BLL.App.DTO.TrackLocation? TrackLocation { get; set; }
        
        public SelectList? Tracks { get; set; } 
    }
}