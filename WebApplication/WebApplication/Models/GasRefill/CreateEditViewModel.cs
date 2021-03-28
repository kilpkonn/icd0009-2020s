using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.GasRefill
{
    public class CreateEditViewModel
    {
        public BLL.App.DTO.GasRefill? GasRefill { get; set; }
        
        public SelectList? Cars { get; set; } 
    }
}