using System.ComponentModel.DataAnnotations;
using Resource.Base;

namespace BLL.App.DTO.Identity
{
    public class Register
    {
        [Required(ErrorMessageResourceType = typeof(Common),
            ErrorMessageResourceName = "ErrorMessage_Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Common),
            ErrorMessageResourceName = "ErrorMessage_Email")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessageResourceType = typeof(Common),
            ErrorMessageResourceName = "ErrorMessage_Required")]
        [StringLength(100, ErrorMessageResourceType = typeof(Common),
            ErrorMessageResourceName = "ErrorMessage_StringLengthMinMax", MinimumLength = 6)]
        public string Password { get; set; } = default!;

        [Required(ErrorMessageResourceType = typeof(Common),
            ErrorMessageResourceName = "ErrorMessage_Required")]
        [StringLength(32, ErrorMessageResourceType = typeof(Common),
            ErrorMessageResourceName = "ErrorMessage_DisplayNameLength", MinimumLength = 1)]
        public string DisplayName { get; set; } = default!;
    }
}