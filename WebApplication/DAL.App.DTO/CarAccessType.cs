using DAL.Base;

namespace DAL.App.DTO
{
    public class CarAccessType : DalEntityId
    {
        public string Name { get; set; } = null!;
        public int AccessLevel { get; set; }
    }
}