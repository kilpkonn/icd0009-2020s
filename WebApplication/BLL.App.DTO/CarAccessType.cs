using BLL.Base;

namespace BLL.App.DTO
{
    public class CarAccessType : BllEntityId
    {
        public string? Name { get; set; } = null!;
        public int? AccessLevel { get; set; }
    }
}