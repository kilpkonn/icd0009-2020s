using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.CarAccess
{
    /// <summary>
    /// Car access view model
    /// </summary>
    public class CreateEditViewModel
    {
        /// <summary>
        /// Car access
        /// </summary>
        public BLL.App.DTO.CarAccess? CarAccess { get; set; }
        /// <summary>
        /// Car options
        /// </summary>
        public SelectList? CarOptions { get; set; } 
        /// <summary>
        /// Car access type options
        /// </summary>
        public SelectList? CarAccessTypeOptions { get; set; }
    }
}