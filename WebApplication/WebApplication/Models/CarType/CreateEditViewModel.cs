using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.CarType
{
    /// <summary>
    /// Car type view model
    /// </summary>
    public class CreateEditViewModel
    {
        /// <summary>
        /// Car type
        /// </summary>
        public BLL.App.DTO.CarType? CarType { get; set; }
        
        /// <summary>
        /// Car model options
        /// </summary>
        public SelectList? CarModels { get; set; } 
    }
}