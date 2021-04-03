using PublicApi.DTO.v1.Base;

namespace PublicApi.DTO.v1
{
    public class CarAccessType : ApiDtoEntityId
    {
        public string? Name { get; set; } = null!;
        public int? AccessLevel { get; set; }
    }
}