using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.Track
{
    /// <summary>
    /// Track view model
    /// </summary>
    public class CreateEditViewModel
    {
        /// <summary>
        /// Track
        /// </summary>
        public BLL.App.DTO.Track? Track { get; set; }
        
        /// <summary>
        /// Car options
        /// </summary>
        public SelectList? Cars { get; set; } 
    }
}