using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.TrackLocation
{
    public class CreateEditViewModel
    {
        public Domain.App.TrackLocation? TrackLocation { get; set; }
        
        public SelectList? Tracks { get; set; } 
    }
}