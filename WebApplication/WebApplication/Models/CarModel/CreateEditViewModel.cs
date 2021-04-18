using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.CarModel
{
    /// <summary>
    /// Car model view model
    /// </summary>
    public class CreateEditViewModel
    {
        /// <summary>
        /// Car model
        /// </summary>
        public BLL.App.DTO.CarModel? CarModel { get; set; }
        
        /// <summary>
        /// Car mark options
        /// </summary>
        public SelectList? CarMarks { get; set; } 
    }
}