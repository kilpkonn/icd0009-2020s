using Domain.Base;

namespace BLL.App.DTO
{
    public class CarAccessType : DomainEntityId
    {
        public string Name { get; set; } = null!;
        public int AccessLevel { get; set; }
    }
}