using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.CarErrorCode
{
    /// <summary>
    /// Car erro code view model
    /// </summary>
    public class CreateEditViewModel
    {
        /// <summary>
        /// Car error code
        /// </summary>
        public BLL.App.DTO.CarErrorCode? CarErrorCode { get; set; }
        /// <summary>
        /// Car options
        /// </summary>
        public SelectList? CarOptions { get; set; } 
    }
}