using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.TrackLocation
{
    /// <summary>
    /// TRack location view model
    /// </summary>
    public class CreateEditViewModel
    {
        /// <summary>
        /// TRack location
        /// </summary>
        public BLL.App.DTO.TrackLocation? TrackLocation { get; set; }
        
        /// <summary>
        /// Track options
        /// </summary>
        public SelectList? Tracks { get; set; } 
    }
}