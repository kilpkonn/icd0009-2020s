using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.Car
{
    /// <summary>
    /// Car create dit vm
    /// </summary>
    public class CreateEditViewModel
    {
        /// <summary>
        /// Car
        /// </summary>
        public BLL.App.DTO.Car? Car { get; set; }
        /// <summary>
        /// Car type options
        /// </summary>
        public SelectList? CarTypeOptions { get; set; }
    }
}