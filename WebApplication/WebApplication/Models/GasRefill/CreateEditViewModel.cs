using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.GasRefill
{
    /// <summary>
    /// Gas refill view model
    /// </summary>
    public class CreateEditViewModel
    {
        /// <summary>
        /// Gas refill
        /// </summary>
        public BLL.App.DTO.GasRefill? GasRefill { get; set; }
        
        /// <summary>
        /// Gar options
        /// </summary>
        public SelectList? Cars { get; set; } 
    }
}