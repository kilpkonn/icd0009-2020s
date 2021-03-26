using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models.GasRefill
{
    public class CreateEditViewModel
    {
        public Domain.App.GasRefill? GasRefill { get; set; }
        
        public SelectList? Cars { get; set; } 
    }
}