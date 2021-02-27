using Domain.Base;

namespace Domain.App
{
    public class CarErrorCode : DomainEntity
    {
        public int CanId { get; set; }
        public long CanData { get; set; }
    }
}